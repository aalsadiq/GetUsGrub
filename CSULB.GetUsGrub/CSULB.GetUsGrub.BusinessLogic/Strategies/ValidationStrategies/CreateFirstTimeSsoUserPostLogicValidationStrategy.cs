using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
// TODO: @Jenn Comment and Unit Test [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class CreateFirstTimeSsoUserPostLogicValidationStrategy
    {
        private readonly UserAccount _userAccount;
        private readonly PasswordSalt _passwordSalt;

        public CreateFirstTimeSsoUserPostLogicValidationStrategy(UserAccount userAccount, PasswordSalt passwordSalt)
        {
            _userAccount = userAccount;
            _passwordSalt = passwordSalt;   
        }

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
                    result.Error = "Something went wrong. Please try again later.";
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
