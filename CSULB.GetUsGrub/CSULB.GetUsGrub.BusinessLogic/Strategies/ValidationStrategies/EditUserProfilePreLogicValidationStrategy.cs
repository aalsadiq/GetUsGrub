using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic.Strategies.ValidationStrategies
{
    public class EditUserProfilePreLogicValidationStrategy
    {
        private readonly UserProfileDto _userProfileDto;
        private readonly UserProfileDtoValidator _userProfileDtoValidator;

        public EditUserProfilePreLogicValidationStrategy(UserProfileDto userProfileDto)
        {
            _userProfileDto = userProfileDto;
            _userProfileDtoValidator = new UserProfileDtoValidator();
        }

        public ResponseDto<UserProfileDto> Execute()
        {
            var validationResult = _userProfileDtoValidator.Validate(_userProfileDto, ruleSet: "EditProfile");
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<UserProfileDto>
                {
                    Data = _userProfileDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }
        }
    }
}
