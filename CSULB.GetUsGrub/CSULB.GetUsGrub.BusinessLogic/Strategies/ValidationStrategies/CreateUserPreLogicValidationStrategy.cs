using CSULB.GetUsGrub.Models;
using FluentValidation;
using GitGrub.GetUsGrub.Models;
using Newtonsoft.Json;

// TODO: @Jenn Unit test ValidationStrategy [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateUserPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for creating a user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class CreateUserPreLogicValidationStrategy
    {
        private readonly UserAccountDtoValidator _userAccountDtoValidator;
        private readonly SecurityQuestionDtoValidator _securityQuestionDtoValidator;
        private readonly UserProfileDtoValidator _userProfileDtoValidator;
        private readonly UserValidator _userValidator;

        public CreateUserPreLogicValidationStrategy()
        {
            _userAccountDtoValidator = new UserAccountDtoValidator();
            _securityQuestionDtoValidator = new SecurityQuestionDtoValidator();
            _userProfileDtoValidator = new UserProfileDtoValidator();
            _userValidator = new UserValidator();
        }

        public ResponseDto<RegisterUserDto> ExecuteIndividualStrategy(RegisterUserDto registerUserDto)
        {
            // Validate UserAccountDto
            var validationResult = _userAccountDtoValidator.Validate(registerUserDto.UserAccountDto, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                var error = JsonConvert.SerializeObject(validationResult.Errors);
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = error
                };
            }

            // Validate SecurityQuestionDtos
            foreach (var securityQuestion in registerUserDto.SecurityQuestionDtos)
            {
                validationResult = _securityQuestionDtoValidator.Validate(securityQuestion, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    var error = JsonConvert.SerializeObject(validationResult.Errors);
                    return new ResponseDto<RegisterUserDto>
                    {
                        Data = registerUserDto,
                        Error = error
                    };
                }
            }

            // Validate UserProfileDto
            validationResult = _userProfileDtoValidator.Validate(registerUserDto.UserProfileDto, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                var error = JsonConvert.SerializeObject(validationResult.Errors);
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = error
                };
            }

            // Validate username and display name are not equal
            var result = _userValidator.CheckIfUsernameEqualsDisplayName(registerUserDto.UserAccountDto.Username, registerUserDto.UserProfileDto.DisplayName);
            if (result)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = "Username must not be the same as display name."
                };
            }

            // Validate user does not exist
            result = _userValidator.CheckIfUserExists(registerUserDto.UserAccountDto.Username);
            if (result)
            {
                return new ResponseDto<RegisterUserDto>
                {
                    Data = registerUserDto,
                    Error = "Username is already used."
                };
            }

            return new ResponseDto<RegisterUserDto>
            {
                Data = registerUserDto
            };
        }

        public ResponseDto<RegisterRestaurantDto> ExecuteRestaurantStrategy(RegisterRestaurantDto registerRestaurantDto)
        {
            var restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
            var addressValidator = new AddressValidator();
            var businessHourValidator = new BusinessHourValidator();

            // Validate base user
            var response = ExecuteIndividualStrategy(registerRestaurantDto);
            if (response.Error != null)
            {
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = response.Error
                };
            }

            // Validate RestaurantProfileDto
            var validationResult = restaurantProfileDtoValidator.Validate(registerRestaurantDto.RestaurantProfileDto, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                var error = JsonConvert.SerializeObject(validationResult.Errors);
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = error
                };
            }

            // Validate Address
            validationResult = addressValidator.Validate(registerRestaurantDto.RestaurantProfileDto.Address, ruleSet: "CreateUser");
            if (!validationResult.IsValid)
            {
                var error = JsonConvert.SerializeObject(validationResult.Errors);
                return new ResponseDto<RegisterRestaurantDto>
                {
                    Data = registerRestaurantDto,
                    Error = error
                };
            }

            // Validate BusinessHour
            foreach (var businessHour in registerRestaurantDto.RestaurantProfileDto.BusinessHours)
            {
                validationResult = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
                if (!validationResult.IsValid)
                {
                    var error = JsonConvert.SerializeObject(validationResult.Errors);
                    return new ResponseDto<RegisterRestaurantDto>
                    {
                        Data = registerRestaurantDto,
                        Error = error
                    };
                }

                var result = businessHourValidator.CheckIfDayIsDayOfWeek(businessHour.Day);
                if (!result)
                {
                    return new ResponseDto<RegisterRestaurantDto>
                    {
                        Data = registerRestaurantDto,
                        Error = "Day must be either Sunday, Monday, Tuesday, Wednesday, Thursday, Friday or Saturday."
                    };
                }

                result = businessHourValidator.CheckIfOpenTimeIsBeforeCloseTime(businessHour.OpenTime,businessHour.CloseTime);
                if (!result)
                {
                    return new ResponseDto<RegisterRestaurantDto>
                    {
                        Data = registerRestaurantDto,
                        Error = "Day must be either Sunday, Monday, Tuesday, Wednesday, Thursday, Friday or Saturday."
                    };
                }
            }

            return new ResponseDto<RegisterRestaurantDto>
            {
                Data = registerRestaurantDto
            };
        }
    }
}
