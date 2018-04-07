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
                RuleFor(RestaurantSelection => RestaurantSelection.City)
                    .NotEmpty().WithMessage("City is required.")
                    .NotNull().WithMessage("City is required.");

                RuleFor(RestaurantSelection => RestaurantSelection.State)
                    .NotEmpty().WithMessage("State is required.")
                    .NotNull().WithMessage("State is required.");

                RuleFor(RestaurantSelection => RestaurantSelection.FoodType)
                    .NotEmpty().WithMessage("Food type is required.")
                    .NotNull().WithMessage("Food type is required.");

                RuleFor(RestaurantSelection => RestaurantSelection.DistanceInMiles)
                    .NotEmpty().WithMessage("Distance is required.")
                    .NotNull().WithMessage("Distance is required.")
                    .Must(distance => distance.Equals(1) | distance.Equals(5) | distance.Equals(10) | distance.Equals(15));

                RuleFor(RestaurantSelection => RestaurantSelection.AvgFoodPrice)
                    .NotNull().WithMessage("Average food price is required");
            });

            RuleSet("UnregisteredUserPostLogic", () =>
            {
                RuleFor(RestaurantSelection => RestaurantSelection.City)
                    .NotEmpty()
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.State)
                    .NotEmpty()
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.FoodType)
                    .NotEmpty()
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.DistanceInMiles)
                    .NotEmpty()
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.AvgFoodPrice)
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.CurrentUtcDateTime)
                    .NotEmpty()
                    .NotNull();
            });

            RuleSet("RegisteredUserPostLogic", () =>
            {
                RuleFor(RestaurantSelection => RestaurantSelection.City)
                    .NotEmpty()
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.State)
                    .NotEmpty()
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.FoodType)
                    .NotEmpty()
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.DistanceInMiles)
                    .NotEmpty()
                    .NotNull();
                
                // TODO: @Rachel Need food preferences to uncomment rules below [-Jenn]
                //RuleFor(x => x.FoodPreferences)
                //    .NotEmpty()
                //    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.AvgFoodPrice)
                    .NotNull();

                RuleFor(RestaurantSelection => RestaurantSelection.CurrentUtcDateTime)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
