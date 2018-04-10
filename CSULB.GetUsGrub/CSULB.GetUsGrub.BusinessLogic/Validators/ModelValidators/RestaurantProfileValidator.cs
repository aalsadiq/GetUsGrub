using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RestaurantProfileValidator</c> class.
    /// Defines rules to validate a RestaurantProfile.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileValidator : AbstractValidator<RestaurantProfile>
    {
        public RestaurantProfileValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(restaurantProfile => restaurantProfile.PhoneNumber)
                    .NotEmpty()
                    .NotNull()
                    .Matches(RegularExpressions.PHONE_NUMBER_FORMAT);

                RuleFor(restaurantProfile => restaurantProfile.Address)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantProfile => restaurantProfile.GeoCoordinates.Latitude)
                    .NotEmpty();

                RuleFor(restaurantProfile => restaurantProfile.GeoCoordinates.Longitude)
                    .NotEmpty();
            });
        }
    }
}
