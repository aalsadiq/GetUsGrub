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
                RuleFor(x => x.Category)
                    .NotEmpty().WithMessage("Restaurant category is required.")
                    .NotNull().WithMessage("Restaurant category is required.");

                RuleFor(x => x.AvgFoodPrice)
                    .NotEmpty().WithMessage("Average food price is required.")
                    .Must(x => (x >= 0 && x <= 2)).WithMessage("Average food price is invalid.");
            });
        }
        // TODO: @Jenn Validate Category is not empty, not null and in enum [-Jenn]
        public ResponseDto<bool> CheckIfCategoryIsRestaurantCategory(string category)
        {
            // True if day is in RestaurantCategories enum
            return new ResponseDto<bool>()
            {
                Data = Enum.IsDefined(typeof(RestaurantCategories), category.Replace(" ", string.Empty))
            };
        }
    }
}