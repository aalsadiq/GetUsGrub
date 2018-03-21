using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RestaurantProfileDtoValidator</c> class.
    /// Defines rules to validate a RestaurantProfileDto.
    /// <para>
    /// @author: Jennifer Nguyen, Andrew Kao
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileDtoValidator : AbstractValidator<RestaurantProfileDto>
    {
        public RestaurantProfileDtoValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.BusinessHours)
                    .NotEmpty().WithMessage("At least one business hour must be filled.")
                    .NotNull().WithMessage("At least one business hour must be filled.");

                RuleFor(x => x.PhoneNumber.ToString())
                    .NotEmpty().WithMessage("Phone number is required.")
                    .NotNull().WithMessage("Phone number is required.")
                    .Matches(@"^\([2-9]\d{2}\)\d{3}\-\d{4}$").WithMessage("Phone number must be in (XXX)XXX-XXXX format.");

                RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Address is required.")
                    .NotNull().WithMessage("Address is required.");
            });

            RuleSet("EditProfile", () =>
            {
                RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Address is required.")
                    .NotNull().WithMessage("Address is required.");

                RuleFor(x => x.PhoneNumber.ToString())
                    .NotEmpty().WithMessage("Phone number is required.")
                    .NotNull().WithMessage("Phone number is required.")
                    .Matches(@"^\([2-9]\d{2}\)\d{3}\-\d{4}$").WithMessage("Phone number must be in (XXX)XXX-XXXX format.");

                RuleFor(x => x.RestaurantMenus)
                    .SetCollectionValidator(new RestaurantMenuValidator());

                RuleFor(x => x.BusinessHours)
                    .SetCollectionValidator(new BusinessHourValidator());

                RuleFor(x => x.RestaurantMenuItems)
                    .SetCollectionValidator(new RestaurantMenuItemValidator());
            });
        }
    }
}