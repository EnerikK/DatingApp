using DatingApp.Domain.Aggregates.UserProfileAggregates;
using FluentValidation;

namespace DatingApp.Domain.Validators.UserProfileValidator;

public class PhotoValidator : AbstractValidator<Photos>
{
    public PhotoValidator()
    {
        RuleFor(info => info.Url).NotNull().WithMessage("Picture must be Url format and its required ");
    }
}