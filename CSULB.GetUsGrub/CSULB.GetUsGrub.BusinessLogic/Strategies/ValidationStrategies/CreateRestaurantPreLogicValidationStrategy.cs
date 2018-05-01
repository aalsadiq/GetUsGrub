using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateRestaurantPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for creating a restaurant.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    public class CreateRestaurantPreLogicValidationStrategy
    {
        private readonly RegisterRestaurantDto _registerRestaurantDto;
        private readonly BusinessHourDtoValidator _businessHourDtoValidator;
        private readonly BusinessHourValidator _businessHourValidator;
        private readonly RestaurantDetailValidator _restaurantDetailValidator;

        public CreateRestaurantPreLogicValidationStrategy(RegisterRestaurantDto registerRestaurantDto)
        {
            _registerRestaurantDto = registerRestaurantDto;
            _businessHourDtoValidator = new BusinessHourDtoValidator();
            _businessHourValidator = new BusinessHourValidator();
            _restaurantDetailValidator = new RestaurantDetailValidator();
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a data transfer object for creating a restaurant user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/20/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {
            ResponseDto<bool> result;

            var validationWrappers = new List<IValidationWrapper>()
            {
                new ValidationWrapper<RestaurantProfileDto>(_registerRestaurantDto.RestaurantProfileDto, "CreateUser", new RestaurantProfileDtoValidator()),
                new ValidationWrapper<Address>(_registerRestaurantDto.RestaurantProfileDto.Address, "CreateUser", new AddressValidator()),
                new ValidationWrapper<RestaurantDetail>(_registerRestaurantDto.RestaurantProfileDto.Details, "CreateUser", _restaurantDetailValidator)
            };

            foreach (var businessHourDto in _registerRestaurantDto.BusinessHourDtos)
            {
                validationWrappers.Add(new ValidationWrapper<BusinessHourDto>(businessHourDto, "CreateUser", _businessHourDtoValidator));
            }

            foreach (var validationWrapper in validationWrappers)
            {
                result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    return result;
                }
            }

            // Validate BusinessHour
            foreach (var businessHourDto in _registerRestaurantDto.BusinessHourDtos)
            {
                result = _businessHourDtoValidator.CheckIfDayIsDayOfWeek(businessHourDto.Day);
                if (!result.Data)
                {
                    result.Error = ValidationErrorMessages.NOT_DAY_OF_WEEK;
                    return result;
                }

                result = _businessHourDtoValidator.CheckIfOpenTimeIsBeforeCloseTime(businessHourDto.OpenTime, businessHourDto.CloseTime);
                if (!result.Data)
                {
                    result.Error = ValidationErrorMessages.OPEN_TIME_MUST_BE_BEFORE_CLOSE_TIME;
                    return result;
                }
            }

            result = _restaurantDetailValidator.CheckIfFoodTypeIsRestaurantFoodType(_registerRestaurantDto.RestaurantProfileDto.Details.FoodType);
            if (!result.Data)
            {
                result.Error = ValidationErrorMessages.INVALID_FOOD_TYPE;
                return result;
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
