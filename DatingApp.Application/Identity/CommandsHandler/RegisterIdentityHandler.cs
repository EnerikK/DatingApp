﻿using AutoMapper;
using DatingApp.Application.Enum;
using DatingApp.Application.Identity.Commands;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using DatingApp.Application.Services;
using DatingApp.DataAccess;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Identity.CommandsHandler
{
    public class RegisterIdentityHandler : IRequestHandler<RegisterIdentity, OperationResult<IdentityUserProfileDto>>
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IdentityService _identityService;
        private OperationResult<IdentityUserProfileDto> _result = new();

        public RegisterIdentityHandler(DataContext dataContext, UserManager<IdentityUser> userManager,
        IdentityService identityService,IMapper mapper)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<OperationResult<IdentityUserProfileDto>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateIdentityDoesNotExist(request);
                if (_result.IsError) return _result;

                await using var transaction = await _dataContext.Database.BeginTransactionAsync(cancellationToken);

                var identity = await CreateIdentityUserAsync(request, transaction, cancellationToken);
                if (_result.IsError) return _result;

                var profile = await CreateUserProfileAsync(request, transaction, identity, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _result.PayLoad = _mapper.Map<IdentityUserProfileDto>(profile);
                _result.PayLoad.UserName = identity.UserName;
                _result.PayLoad.Token = GetJWTString(identity, profile);
                return _result;
            }
            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }
            return _result;
        }
        private async Task ValidateIdentityDoesNotExist(RegisterIdentity request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Username);

            if (existingIdentity != null) _result.AddError(ErrorCode.IdentityUserAlreadyExists, ErrorMessages.UserAlreadyExists);
        }

        private async Task<IdentityUser> CreateIdentityUserAsync(RegisterIdentity request, IDbContextTransaction transaction,
            CancellationToken cancellationToken)
        {
            var identity = new IdentityUser { Email = request.Username, UserName = request.Username };
            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                foreach (var identityError in createdIdentity.Errors)
                {
                    _result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
                }
            }
            return identity;
        }
        private async Task<UserProfile> CreateUserProfileAsync(RegisterIdentity request, IDbContextTransaction transaction,
            IdentityUser identity, CancellationToken cancellationToken)
        {
            try
            {
                var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.Username,
                    request.Phone, request.DateOfBirth, request.CurrentCity);

                var profile = UserProfile.CreateUserProfile(identity.Id, profileInfo);
                _dataContext.UserProfiles.Add(profile);
                await _dataContext.SaveChangesAsync(cancellationToken);
                return profile;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

        }
        private string GetJWTString(IdentityUser identityUser, UserProfile userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
            new Claim("IdentityId", identityUser.Id),
            new Claim("UserProfileId", userProfile.UserProfileId.ToString())
            });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
    }
}