using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RestaurantProfileDtoValidator</c> class.
    /// Defines rules to validate a RestaurantProfileDto.
    /// <para>
    /// @author: Jennifer Nguyen, Andrew Kao
    /// @updated: 03/18/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileDtoValidator : AbstractValidator<RestaurantProfileDto>
    {
        public RestaurantProfileDtoValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .NotEmpty().WithMessage("Phone number is required.")
                    .NotNull().WithMessage("Phone number is required.")
                    .Matches(@"^\([2-9]\d{2}\)\d{3}\-\d{4}$").WithMessage("Phone number must be in (XXX)XXX-XXXX format.");

                RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Address is required.")
                    .NotNull().WithMessage("Address is required.");
            });

            RuleSet("EditProfile", () =>
            {
                RuleFor(x => x.RestaurantName)
                    .NotEmpty().WithMessage("Restaurant name is required.")
                    .NotNull().WithMessage("Restaurant name is required.");

                RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Address is required.")
                    .NotNull().WithMessage("Address is required.");

                RuleFor(x => x.PhoneNumber.ToString())
                    .NotEmpty().WithMessage("Phone number is required.")
                    .NotNull().WithMessage("Phone number is required.")
                    .Matches(@"^\([2-9]\d{2}\)\d{3}\-\d{4}$").WithMessage("Phone number must be in (XXX)XXX-XXXX format.");

                RuleFor(x => x.Menus)
                    .SetCollectionValidator(new RestaurantMenuValidator());

                RuleFor(x => x.BusinessHours)
                    .SetCollectionValidator(new BusinessHourValidator());
            });
        }
    }
}