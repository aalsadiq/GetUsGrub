using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Defines the validation strategy before executing logic to edit food preferences
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/24/18
    /// </summary>
    public class EditFoodPreferencesValidationStrategy
    {
        // Declarations
        private readonly FoodPreferencesDto _foodPreferencesDto;
        private readonly FoodPreferencesDtoValidator _foodPreferencesDtoValidator;

        // Constructor
        public EditFoodPreferencesValidationStrategy(FoodPreferencesDto foodPreferencesDto)
        {
            _foodPreferencesDto = foodPreferencesDto;
            _foodPreferencesDtoValidator = new FoodPreferencesDtoValidator();
        }

        // Execution strategy
        public ResponseDto<FoodPreferencesDto> ExecuteStrategy()
        {
            // Validate the dto
            var result = _foodPreferencesDtoValidator.Validate(_foodPreferencesDto, ruleSet: "EditFoodPreferences");

            // If the validation result is invalid, return a response dto with an error
            if (!result.IsValid)
            {
                var errorsList = new List<string>();
                result.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<FoodPreferencesDto>
                {
                    Data = _foodPreferencesDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }

            // If no error occurs, return a response dto with no errors
            return new ResponseDto<FoodPreferencesDto>
            {
                Data = _foodPreferencesDto
            };
        }
    }
}
