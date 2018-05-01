using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateFirstTimeSsoUserPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for creating a first time SSO user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/04/2018
    /// </para>
    /// </summary>
    public class CreateFirstTimeSsoUserPreLogicValidationStrategy
    {
        private readonly UserAccountDto _userAccountDto;
        private readonly UserAccountDtoValidator _userAccountDtoValidator;
        private readonly UserValidator _userValidator;

        public CreateFirstTimeSsoUserPreLogicValidationStrategy(UserAccountDto userAccountDto)
        {
            _userAccountDto = userAccountDto;
            _userAccountDtoValidator = new UserAccountDtoValidator();
            _userValidator = new UserValidator();
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a UserAccountDto.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/04/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrapper = new ValidationWrapper<UserAccountDto>(data: _userAccountDto, ruleSet: "SsoRegistration", validator: _userAccountDtoValidator);
            var result = validationWrapper.ExecuteValidator();
            if (!result.Data)
            {
                return result;
            }

            result = _userValidator.CheckIfUserExists(_userAccountDto.Username);
            if (result.Data)
            {
                result.Error = UserManagementErrorMessages.USER_EXISTS;
            }

            return result;
        }
    }
}
