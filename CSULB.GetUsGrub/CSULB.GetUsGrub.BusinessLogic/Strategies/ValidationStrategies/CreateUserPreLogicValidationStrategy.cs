using CSULB.GetUsGrub.Models;
using FluentValidation;
using GitGrub.GetUsGrub.Models;
using Newtonsoft.Json;

// TODO: @Jenn Unit test ValidationStrategy [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateUserPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for creating users.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class CreateUserPreLogicValidationStrategy
    {
        private readonly RegisterUserDto _registerUserDto;
        private readonly UserAccountDtoValidator _userAccountDtoValidator;
        private readonly SecurityQuestionDtoValidator _securityQuestionDtoValidator;
        private readonly UserProfileDtoValidator _userProfileDtoValidator;
        private readonly UserValidator _userValidator;

        public CreateUserPreLogicValidationStrategy(RegisterUserDto registerUserDto)
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
                var error = JsonConvert.SerializeObject(validationResult.Errors);
                var responseDto = new ResponseDto<RegisterUserDto>
                {
                    Data = _registerUserDto,
                    Error = error
                };
                return responseDto;
            }

            // Validate SecurityQuestionDtos
            foreach (var securityQuestion in _registerUserDto.SecurityQuestionDtos)
            {
                validationResult = _securityQuestionDtoValidator.Validate(securityQuestion, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    var error = JsonConvert.SerializeObject(validationResult.Errors);
                    var responseDto = new ResponseDto<RegisterUserDto>
                    {
                        Data = _registerUserDto,
                        Error = error
                    };
                    return responseDto;
                }
            }

            // Validate UserProfileDto
            validationResult = _userProfileDtoValidator.Validate(_registerUserDto.UserProfileDto, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                var error = JsonConvert.SerializeObject(validationResult.Errors);
                var responseDto = new ResponseDto<RegisterUserDto>
                {
                    Data = _registerUserDto,
                    Error = error
                };
                return responseDto;
            }

            // Validate username and display name are not equal
            var result = _userValidator.CheckIfUsernameEqualsDisplayName(_registerUserDto.UserAccountDto.Username, _registerUserDto.UserProfileDto.DisplayName);
            if (result)
            {
                var responseDto = new ResponseDto<RegisterUserDto>
                {
                    Data = _registerUserDto,
                    Error = "Username must not be the same as display name."
                };
                return responseDto;
            }

            // Validate user does not exist
            result = _userValidator.CheckIfUserExists(_registerUserDto.UserAccountDto.Username);
            if (result)
            {
                var responseDto = new ResponseDto<RegisterUserDto>
                {
                    Data = _registerUserDto,
                    Error = "Username is already used."
                };
                return responseDto;
            }

            return new ResponseDto<RegisterUserDto>
            {
                Data = _registerUserDto
            };
        }
    }
}
