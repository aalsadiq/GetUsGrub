using CSULB.GetUsGrub.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Validates contents of user profile DTO
    /// @author: Andrew
    /// @updated: 3/18/18
    /// </summary>
    public class EditUserProfilePreLogicValidationStrategy
    {
        private readonly UserProfileDto _userProfileDto;
        private readonly UserProfileDtoValidator _userProfileDtoValidator;

        public EditUserProfilePreLogicValidationStrategy(UserProfileDto userProfileDto)
        {
            _userProfileDto = userProfileDto;
            _userProfileDtoValidator = new UserProfileDtoValidator();
        }

        public ResponseDto<UserProfileDto> ExecuteStrategy()
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

            return new ResponseDto<UserProfileDto>
            {
                Data = _userProfileDto,
                Error = null
            };
        }
    }
}