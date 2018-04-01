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

                // TODO: @Jenn Check that the distance is 1, 5, 10, or 15 [-Jenn]
                RuleFor(x => x.Distance)
                    .NotEmpty().WithMessage("Distance is required.")
                    .NotNull().WithMessage("Distance is required.");

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

                RuleFor(x => x.Distance)
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

                RuleFor(x => x.Distance)
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
