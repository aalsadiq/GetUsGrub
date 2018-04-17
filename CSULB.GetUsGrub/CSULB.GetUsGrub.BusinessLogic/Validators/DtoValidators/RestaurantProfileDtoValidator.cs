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
                RuleFor(restaurantProfileDto => restaurantProfileDto.PhoneNumber)
                    .NotEmpty().WithMessage(ValidationErrorMessages.PHONE_NUMBER_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.PHONE_NUMBER_REQUIRED)
                    .Matches(RegularExpressions.PHONE_NUMBER_FORMAT).WithMessage(ValidationErrorMessages.PHONE_NUMBER_FORMAT);

                RuleFor(restaurantProfileDto => restaurantProfileDto.Address)
                    .NotEmpty().WithMessage(ValidationErrorMessages.ADDRESS_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.ADDRESS_REQUIRED);
            });

            RuleSet("EditProfile", () =>
            {
                RuleFor(RestaurantProfile => RestaurantProfile.Address)
                    .NotEmpty().WithMessage("Address is required.")
                    .NotNull().WithMessage("Address is required.");

                RuleFor(RestaurantProfile => RestaurantProfile.PhoneNumber.ToString())
                    .NotEmpty().WithMessage("Phone number is required.")
                    .NotNull().WithMessage("Phone number is required.")
                    .Matches(@"^\([2-9]\d{2}\)\d{3}\-\d{4}$").WithMessage("Phone number must be in (XXX)XXX-XXXX format.");

                //RuleFor(RestaurantProfile => RestaurantProfile.MenuDictionary).SetCollectionValidator(new RestaurantMenuValidator());

                RuleFor(RestaurantProfile => RestaurantProfile.BusinessHours)
                    .SetCollectionValidator(new BusinessHourValidator());
            });
        }
    }
}
