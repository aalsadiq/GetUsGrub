using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateFirstTimeSsoUserPostLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for creating a first time SSO user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/04/2018
    /// </para>
    /// </summary>
    public class CreateFirstTimeSsoUserPostLogicValidationStrategy
    {
        private readonly UserAccount _userAccount;
        private readonly PasswordSalt _passwordSalt;

        public CreateFirstTimeSsoUserPostLogicValidationStrategy(UserAccount userAccount, PasswordSalt passwordSalt)
        {
            _userAccount = userAccount;
            _passwordSalt = passwordSalt;   
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a UserAccount and PasswordSalt model.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/04/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrappers = new List<IValidationWrapper>()
            {
                new ValidationWrapper<UserAccount>(_userAccount, "SsoRegistration", new UserAccountValidator()),
                new ValidationWrapper<PasswordSalt>(_passwordSalt, "SsoRegistration", new PasswordSaltValidator())
            };

            foreach (var validationWrapper in validationWrappers)
            {
                var result = validationWrapper.ExecuteValidator();
                if (!result.Data)
                {
                    result.Error = GeneralErrorMessages.GENERAL_ERROR;
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
