using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>ResetPasswordPostLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for reseting a user's password.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/25/2018
    /// </para>
    /// </summary>
    public class ResetPasswordPostLogicValidationStrategy
    {
        private readonly UserAccount _userAccount;
        private readonly PasswordSalt _passwordSalt;

        public ResetPasswordPostLogicValidationStrategy(UserAccount userAccount, PasswordSalt passwordSalt)
        {
            _userAccount = userAccount;
            _passwordSalt = passwordSalt;
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate domain models for resetting a password.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <returns>A boolean</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrappers = new List<IValidationWrapper>()
            {
                new ValidationWrapper<UserAccount>(_userAccount, "ResetPassword", new UserAccountValidator()),
                new ValidationWrapper<PasswordSalt>(_passwordSalt, new PasswordSaltValidator()),
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
