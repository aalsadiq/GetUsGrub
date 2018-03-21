using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateRestaurantPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for creating a restaurant.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class CreateRestaurantPreLogicValidationStrategy
    {
        private readonly RegisterRestaurantDto _registerRestaurantDto;
        private readonly CreateIndividualPreLogicValidationStrategy _createIndividualPreLogicValidationStrategy;
        private readonly RestaurantProfileDtoValidator _restaurantProfileDtoValidator;
        private readonly AddressValidator _addressValidator;
        private readonly BusinessHourValidator _businessHourValidator;

        public CreateRestaurantPreLogicValidationStrategy(RegisterRestaurantDto registerRestaurantDto)
        {
            _registerRestaurantDto = registerRestaurantDto;
            _createIndividualPreLogicValidationStrategy = new CreateIndividualPreLogicValidationStrategy(registerRestaurantDto);
            _restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
            _addressValidator = new AddressValidator();
            _businessHourValidator = new BusinessHourValidator();
        }
        public ResponseDto<RegisterRestaurantDto> ExecuteStrategy()
        {
            // Validate base user DTO
            var response = _createIndividualPreLogicValidationStrategy.ExecuteStrategy();
            if (response.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = _registerRestaurantDto,
                    Error =  response.Error
                };
            }
            // Validate RestaurantProfileDto
            var validationResult = _restaurantProfileDtoValidator.Validate(_registerRestaurantDto.RestaurantProfileDto, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = _registerRestaurantDto,
                    Error = JsonConvert.SerializeObject(errors)
            };
            }

            // Validate Address
            System.Diagnostics.Debug.WriteLine("Address Validate2");
            validationResult = _addressValidator.Validate(_registerRestaurantDto.RestaurantProfileDto.Address, ruleSet: "CreateUser");
            System.Diagnostics.Debug.WriteLine("Address Validate2");
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = _registerRestaurantDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }

            // Validate BusinessHour
            foreach (var businessHour in _registerRestaurantDto.RestaurantProfileDto.BusinessHours)
            {
                validationResult = _businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    var errorsList = new List<string>();
                    validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                    var errors = JsonConvert.SerializeObject(errorsList);
                    return new ResponseDto<RegisterRestaurantDto>
                    {
                        Data = _registerRestaurantDto,
                        Error = JsonConvert.SerializeObject(errors)
                    };
                }

                var result = _businessHourValidator.CheckIfDayIsDayOfWeek(businessHour.Day);
                if (!result)
                {
                    return new ResponseDto<RegisterRestaurantDto>
                    {
                        Data = _registerRestaurantDto,
                        Error = "Day must be either Sunday, Monday, Tuesday, Wednesday, Thursday, Friday or Saturday."
                    };
                }

                result = _businessHourValidator.CheckIfOpenTimeIsBeforeCloseTime(businessHour.OpenTime, businessHour.CloseTime);
                if (!result)
                {
                    return new ResponseDto<RegisterRestaurantDto>
                    {
                        Data = _registerRestaurantDto,
                        Error = "Opening time must be less than closing time."
                    };
                }
            }

            return new ResponseDto<RegisterRestaurantDto>
            {
                Data = _registerRestaurantDto
            };
        }
    }
}
