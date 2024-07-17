using DatingApp.Domain.Aggregates.UserProfileAggregates;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Validators.UserProfileValidator
{
    public class BasicInfoValidator : AbstractValidator<BasicInfo>
    {
        public BasicInfoValidator()
        {
            RuleFor(info => info.FirstName).NotNull().WithMessage("FirstName is Required")
            .MinimumLength(3).WithMessage("FirstName must be at least 3 characters")
            .MaximumLength(20).WithMessage("FirstName must be at most 20 characters");

            RuleFor(info => info.LastName).NotNull().WithMessage("LastName is Required")
                .MinimumLength(3).WithMessage("LastName must be at least 3 characters")
                .MaximumLength(20).WithMessage("LastName must be at most 20 characters");

            RuleFor(info => info.EmailAddress).NotNull().WithMessage("EmailAddress is Required")
                .EmailAddress().WithMessage("Provided email is in wrong format");

            RuleFor(info => info.DateOfBirth).InclusiveBetween(
                new DateTime(DateTime.Now.AddYears(-200).Ticks), new DateTime(DateTime.Now.AddYears(-15).Ticks)).WithMessage("You have to be at least 15 years old");
        }
    }
}
