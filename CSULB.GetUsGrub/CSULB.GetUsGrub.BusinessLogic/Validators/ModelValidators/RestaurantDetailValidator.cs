using CSULB.GetUsGrub.Models;
using FluentValidation;
using System;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RestaurantDetailValidator</c> class.
    /// Defines rules to validate a RestaurantDetail.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class RestaurantDetailValidator : AbstractValidator<RestaurantDetail>
    {
        public RestaurantDetailValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(restaurantDetail => restaurantDetail.FoodType)
                    .NotEmpty().WithMessage(ValidationErrorMessages.FOOD_TYPE_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.FOOD_TYPE_REQUIRED);

                RuleFor(restaurantDetail => restaurantDetail.AvgFoodPrice)
                    .NotNull().WithMessage(ValidationErrorMessages.AVG_FOOD_PRICE_REQUIRED)
                    .Must(avgFoodPrice => (avgFoodPrice >= 1 && avgFoodPrice <= 3)).WithMessage(ValidationErrorMessages.NOT_VALID_AVG_FOOD_PRICE);
            });
        }
        
        /// <summary>
        /// The CheckIfFoodTypeIsARestaurantFoodType method.
        /// Checks if the food type is in the enum RestaurantFoodTypes.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="foodType"></param>
        /// <returns>ResponseDto with bool data</returns>
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