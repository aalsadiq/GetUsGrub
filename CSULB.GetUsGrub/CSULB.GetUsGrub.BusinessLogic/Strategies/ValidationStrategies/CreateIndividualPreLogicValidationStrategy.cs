using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

// TODO: @Jenn Unit test ValidationStrategy [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateIndividualPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for creating an individual user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class CreateIndividualPreLogicValidationStrategy
    {
        private readonly RegisterUserDto _registerUserDto;
        private readonly UserAccountDtoValidator _userAccountDtoValidator;
        private readonly SecurityQuestionDtoValidator _securityQuestionDtoValidator;
        private readonly UserProfileDtoValidator _userProfileDtoValidator;
        private readonly UserValidator _userValidator;

        public CreateIndividualPreLogicValidationStrategy(RegisterUserDto registerUserDto)
        {
            _registerUserDto = registerUserDto;
            _userAccountDtoValidator = new UserAccountDtoValidator();
            _securityQuestionDtoValidator = new SecurityQuestionDtoValidator();
            _userProfileDtoValidator = new UserProfileDtoValidator();
            _userValidator = new UserValidator();
        }

        public ResponseDto<RegisterUserDto> ExecuteStrategy()
        {
            // Validate UserAccountDto
            var validationResult = _userAccountDtoValidator.Validate(_registerUserDto.UserAccountDto, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<RegisterUserDto>
                {
                    Data = _registerUserDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }

            // Validate SecurityQuestionDtos
            foreach (var securityQuestion in _registerUserDto.SecurityQuestionDtos)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                validationResult = _securityQuestionDtoValidator.Validate(securityQuestion, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<RegisterUserDto>
                    {
                        Data = _registerUserDto,
                        Error = JsonConvert.SerializeObject(errors)
                    };
                }
            }

            // Validate UserProfileDto
            validationResult = _userProfileDtoValidator.Validate(_registerUserDto.UserProfileDto, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<RegisterUserDto>
                {
                    Data = _registerUserDto,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }

            // Validate username and display name are not equal
            var result = _userValidator.CheckIfUsernameEqualsDisplayName(_registerUserDto.UserAccountDto.Username, _registerUserDto.UserProfileDto.DisplayName);
            if (result)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = _registerUserDto,
                    Error = "Username must not be the same as display name."
                };
            }

            // Validate user does not exist
            result = _userValidator.CheckIfUserExists(_registerUserDto.UserAccountDto.Username);
            System.Diagnostics.Debug.WriteLine("UserManager1231");
            if (result)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = _registerUserDto,
                    Error = "Username is already used."
                };
            }
            System.Diagnostics.Debug.WriteLine("CreateIndividual1");
            return new ResponseDto<RegisterUserDto>
            {
                Data = _registerUserDto
            };
        }
    }
}
