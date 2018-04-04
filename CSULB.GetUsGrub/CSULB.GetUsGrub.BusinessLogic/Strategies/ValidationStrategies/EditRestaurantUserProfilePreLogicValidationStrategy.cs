using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Validates contents of restaurant profile DTO
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class EditRestaurantUserProfilePreLogicValidationStrategy
    {
        private readonly RestaurantProfileDto _restaurantProfileDto;
        private readonly RestaurantProfileDtoValidator _restaurantProfileDtoValidator;

        public EditRestaurantUserProfilePreLogicValidationStrategy(RestaurantProfileDto restaurantProfileDto)
        {
            _restaurantProfileDto = restaurantProfileDto;
            _restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
        }

        public ResponseDto<RestaurantProfileDto> ExecuteStrategy()
        {
            var validationResult = _restaurantProfileDtoValidator.Validate(_restaurantProfileDto, ruleSet: "EditProfile");
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<RestaurantProfileDto>
                {
                    Data = _restaurantProfileDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }

            return new ResponseDto<RestaurantProfileDto>
            {
                Data = _restaurantProfileDto,
                Error = null
            };
        }
    }
}