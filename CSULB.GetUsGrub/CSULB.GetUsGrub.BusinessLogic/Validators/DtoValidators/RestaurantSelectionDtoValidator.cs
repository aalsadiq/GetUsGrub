using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    // TODO: @Jenn Unit test this [-Jenn]
    public class RestaurantSelectionDtoValidator : AbstractValidator<RestaurantSelectionDto>
    {
        public RestaurantSelectionDtoValidator()
        {
            RuleSet("PreLogic", () =>
            {
                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("City is required.")
                    .NotNull().WithMessage("City is required.");

                RuleFor(x => x.State)
                    .NotEmpty().WithMessage("State is required.")
                    .NotNull().WithMessage("State is required.");

                RuleFor(x => x.FoodType)
                    .NotEmpty().WithMessage("Food type is required.")
                    .NotNull().WithMessage("Food type is required.");

                RuleFor(x => x.DistanceInMiles)
                    .NotEmpty().WithMessage("Distance is required.")
                    .NotNull().WithMessage("Distance is required.")
                    .Must(distance => distance.Equals(1) | distance.Equals(5) | distance.Equals(10) | distance.Equals(15));

                RuleFor(x => x.AvgFoodPrice)
                    .NotNull().WithMessage("Average food price is required");
            });

            RuleSet("UnregisteredUserPostLogic", () =>
            {
                RuleFor(x => x.City)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.State)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.FoodType)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.DistanceInMiles)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.AvgFoodPrice)
                    .NotNull();

                RuleFor(x => x.CurrentUtcDateTime)
                    .NotEmpty()
                    .NotNull();
            });

            RuleSet("RegisteredUserPostLogic", () =>
            {
                RuleFor(x => x.City)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.State)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.FoodType)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.DistanceInMiles)
                    .NotEmpty()
                    .NotNull();
                
                // TODO: @Rachel Need food preferences to uncomment rules below [-Jenn]
                //RuleFor(x => x.FoodPreferences)
                //    .NotEmpty()
                //    .NotNull();

                RuleFor(x => x.AvgFoodPrice)
                    .NotNull();

                RuleFor(x => x.CurrentUtcDateTime)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
