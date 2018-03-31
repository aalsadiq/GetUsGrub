using CSULB.GetUsGrub.Models;
using FluentValidation;
using System;
// TODO: @Jenn Comment this yo and unit test [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class RestaurantDetailValidator : AbstractValidator<RestaurantDetail>
    {
        public RestaurantDetailValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.FoodType)
                    .NotEmpty().WithMessage("Restaurant food type is required.")
                    .NotNull().WithMessage("Restaurant food type is required.");

                RuleFor(x => x.AvgFoodPrice)
                    .NotNull().WithMessage("Average food price is required.")
                    .Must(x => (x >= 0 && x <= 2)).WithMessage("Average food price is invalid.");
            });
        }
        // TODO: @Jenn Validate Category is not empty, not null and in enum [-Jenn]
        public ResponseDto<bool> CheckIfFoodTypeIsRestaurantFoodType(string foodType)
        {
            // True if day is in RestaurantCategories enum
            return new ResponseDto<bool>()
            {
                Data = Enum.IsDefined(typeof(RestaurantFoodTypes), foodType.Replace(" ", string.Empty))
            };
        }
    }
}