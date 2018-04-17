using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class UsernameValidationStrategy
    {
        private readonly UserAccountDto _userAccountDto; 
        private readonly UserAccountDtoValidator _userAccountDtoValidator;
        private readonly UserValidator _userValidator;

        /// <summary>
        /// Defines a strategy for validating models before processing business logic 
        /// before preforming CRUD Operations.
        /// The constructor
        /// @author Angelica Salas Tovar, Jennifer Nguyen
        /// </summary>
        /// <param name="userAccountDto"></param>
        public UsernameValidationStrategy(UserAccountDto userAccount)
        {
            _userAccountDto = userAccount;
            _userAccountDtoValidator = new UserAccountDtoValidator();
            _userValidator = new UserValidator();
        }
        
        // Executes the username strategy
        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrapper = new ValidationWrapper<UserAccountDto>(_userAccountDto, "Username", _userAccountDtoValidator);
            var result = validationWrapper.ExecuteValidator();
            if (!result.Data)
            {
                result.Error = ValidationErrorMessages.INVALID_USERNAME;
                return result;
            }
 
            // Validate user does exist
             result = _userValidator.CheckIfUserExists(_userAccountDto.Username);
            if (!result.Data)
            {
                if (result.Error == null)
                {
                    result.Error = ValidationErrorMessages.USER_DOES_NOT_EXIST;
                }

                result.Data = false;
                return result;
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
