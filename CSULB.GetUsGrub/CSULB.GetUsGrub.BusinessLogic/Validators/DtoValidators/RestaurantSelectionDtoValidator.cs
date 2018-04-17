using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RestaurantSelectionDtoValidator</c> class.
    /// Defines rules to validate a RestaurantSelectionDto.
    /// Includes the pre-logic and post-logic validations.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/04/2018
    /// </para>
    /// </summary>
    public class RestaurantSelectionDtoValidator : AbstractValidator<RestaurantSelectionDto>
    {
        public RestaurantSelectionDtoValidator()
        {
            RuleSet("PreLogic", () =>
            {
                RuleFor(restaurantSelectionDto => restaurantSelectionDto.City)
                    .NotEmpty().WithMessage(ValidationErrorMessages.CITY_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.CITY_REQUIRED);

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.State)
                    .NotEmpty().WithMessage(ValidationErrorMessages.STATE_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.STATE_REQUIRED);

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.FoodType)
                    .NotEmpty().WithMessage(ValidationErrorMessages.FOOD_TYPE_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.FOOD_TYPE_REQUIRED);

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.DistanceInMiles)
                    .NotEmpty().WithMessage(ValidationErrorMessages.DISTANCE_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.DISTANCE_REQUIRED)
                    .Must(distance => distance.Equals(1) | distance.Equals(5) | distance.Equals(10) | distance.Equals(15)).WithMessage(ValidationErrorMessages.NOT_VALID_DISTANCE);

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.AvgFoodPrice)
                    .NotNull().WithMessage(ValidationErrorMessages.AVG_FOOD_PRICE_REQUIRED)
                    .Must(avgFoodPrice => (avgFoodPrice >= 1 && avgFoodPrice <= 3)).WithMessage(ValidationErrorMessages.NOT_VALID_AVG_FOOD_PRICE);
            });

            RuleSet("UnregisteredUserPostLogic", () =>
            {
                RuleFor(restaurantSelectionDto => restaurantSelectionDto.City)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.State)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.FoodType)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.DistanceInMiles)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.AvgFoodPrice)
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.CurrentUtcDateTime)
                    .NotEmpty()
                    .NotNull();
            });

            RuleSet("RegisteredUserPostLogic", () =>
            {
                RuleFor(restaurantSelectionDto => restaurantSelectionDto.City)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.State)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.FoodType)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.DistanceInMiles)
                    .NotEmpty()
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.FoodPreferences)
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.AvgFoodPrice)
                    .NotNull();

                RuleFor(restaurantSelectionDto => restaurantSelectionDto.CurrentUtcDateTime)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
