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
                RuleFor(restaurantProfileDto => restaurantProfileDto.PhoneNumber)
                     .NotEmpty().WithMessage(ValidationErrorMessages.PHONE_NUMBER_REQUIRED)
                     .NotNull().WithMessage(ValidationErrorMessages.PHONE_NUMBER_REQUIRED)
                     .Matches(RegularExpressions.PHONE_NUMBER_FORMAT).WithMessage(ValidationErrorMessages.PHONE_NUMBER_FORMAT);

                RuleFor(restaurantProfileDto => restaurantProfileDto.Address)
                    .NotEmpty().WithMessage(ValidationErrorMessages.ADDRESS_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.ADDRESS_REQUIRED);
            });
        }
    }
}
