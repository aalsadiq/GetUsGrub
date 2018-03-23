using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

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
        private readonly SecurityQuestionDtoValidator _securityQuestionDtoValidator;
        private readonly UserValidator _userValidator;

        /// <summary>
        /// Constructor for CreateIndividualPreLogicValidationStrategy.
        /// <para>
        /// @author: Jennifer
        /// @update: 03/18/2018
        /// </para>
        /// </summary>
        /// <param name="registerUserDto"></param>
        public CreateIndividualPreLogicValidationStrategy(RegisterUserDto registerUserDto)
        {
            _registerUserDto = registerUserDto;
            _securityQuestionDtoValidator = new SecurityQuestionDtoValidator();
            _userValidator = new UserValidator();
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a data transfer object for creating an individual user.
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
                new ValidationWrapper<UserAccountDto>(_registerUserDto.UserAccountDto, "CreateUser", new UserAccountDtoValidator()),
                new ValidationWrapper<UserProfileDto>(_registerUserDto.UserProfileDto, "CreateUser", new UserProfileDtoValidator())

            };
            
            foreach (var securityQuestionDto in _registerUserDto.SecurityQuestionDtos)
            {
                validationWrappers.Add(new ValidationWrapper<SecurityQuestionDto>(securityQuestionDto, "CreateUser", _securityQuestionDtoValidator));
            }

            foreach (var validationWrapper in validationWrappers)
            {
                result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    return result;
                }
            }

            // Validate username and display name are not equal
            result = _userValidator.CheckIfUsernameEqualsDisplayName(_registerUserDto.UserAccountDto.Username, _registerUserDto.UserProfileDto.DisplayName);
            if (result.Data)
            {
                result.Error = "Username must not be the same as display name.";
                return result;
            }

            // Validate user does not exist
            result = _userValidator.CheckIfUserExists(_registerUserDto.UserAccountDto.Username);
            if (result.Data)
            {
                result.Error = "Username is already used.";
                return result;
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
