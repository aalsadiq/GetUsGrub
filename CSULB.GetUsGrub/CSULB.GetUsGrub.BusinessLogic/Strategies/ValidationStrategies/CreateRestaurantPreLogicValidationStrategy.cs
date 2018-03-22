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
        private readonly CreateIndividualPreLogicValidationStrategy _createIndividualPreLogicValidationStrategy;

        public CreateRestaurantPreLogicValidationStrategy(RegisterRestaurantDto registerRestaurantDto)
        {
            _registerRestaurantDto = registerRestaurantDto;
            _businessHourDtoValidator = new BusinessHourDtoValidator();
            _businessHourValidator = new BusinessHourValidator();
            _restaurantDetailValidator = new RestaurantDetailValidator();
            _createIndividualPreLogicValidationStrategy = new CreateIndividualPreLogicValidationStrategy(registerRestaurantDto);
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
            // Validate base user DTO
            var result = _createIndividualPreLogicValidationStrategy.ExecuteStrategy();
            if (!result.Data)
            {
                return result;
            }
            System.Diagnostics.Debug.WriteLine("strategy");
            var validationWrappers = new List<IValidationWrapper>()
            {
                new ValidationWrapper<RestaurantProfileDto>(_registerRestaurantDto.RestaurantProfileDto, "CreateUser", new RestaurantProfileDtoValidator()),
                new ValidationWrapper<Address>(_registerRestaurantDto.RestaurantProfileDto.Address, "CreateUser", new AddressValidator()),
                new ValidationWrapper<RestaurantDetail>(_registerRestaurantDto.RestaurantProfileDto.Details, "CreateUser", _restaurantDetailValidator)
            };
            System.Diagnostics.Debug.WriteLine("strategy1");
            foreach (var businessHourDto in _registerRestaurantDto.BusinessHourDtos)
            {
                validationWrappers.Add(new ValidationWrapper<BusinessHourDto>(businessHourDto, "CreateUser", _businessHourDtoValidator));
            }
            System.Diagnostics.Debug.WriteLine("strategy2");
            foreach (var validationWrapper in validationWrappers)
            {
                System.Diagnostics.Debug.WriteLine("strategy3");
                result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    return result;
                }
            }
            System.Diagnostics.Debug.WriteLine("strategy4");
            // Validate BusinessHour
            foreach (var businessHour in _registerRestaurantDto.BusinessHourDtos)
            {
                result = _businessHourValidator.CheckIfDayIsDayOfWeek(businessHour.Day);
                if (!result.Data)
                {
                    result.Error = "Day must be either Sunday, Monday, Tuesday, Wednesday, Thursday, Friday or Saturday.";
                    return result;
                }

                result = _businessHourValidator.CheckIfOpenTimeIsBeforeCloseTime(businessHour.OpenTime, businessHour.CloseTime);
                if (!result.Data)
                {
                    result.Error = "Opening time must be less than closing time.";
                    return result;
                }
            }
            System.Diagnostics.Debug.WriteLine("strategy5");
            result = _restaurantDetailValidator.CheckIfCategoryIsRestaurantCategory(_registerRestaurantDto.RestaurantProfileDto.Details.Category);
            if (!result.Data)
            {
                result.Error = "Category must be a valid restaurant category.";
                return result;
            }
            System.Diagnostics.Debug.WriteLine("strategy6");
            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
