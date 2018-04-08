using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class UsernameValidationStrategy
    {
        private readonly UserAccountDto _userAccountDto; //Private read only userAccountDto
        private readonly UserAccountDtoValidator _userAccountDtoValidator;

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
        }
        
        //Executes the username strategy
        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrappers = new List<IValidationWrapper>()
            {
                new ValidationWrapper<UserAccountDto>(_userAccountDto, "Username", _userAccountDtoValidator)
            };

            foreach (var validationWrapper in validationWrappers)
            {
                var result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    result.Error = "Invalid username, please try again.";
                    return result;
                }
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
