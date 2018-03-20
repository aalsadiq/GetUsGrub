using CSULB.GetUsGrub.Models;
// TODO: @Jenn Comment and unit test [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
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

        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrapper = new ValidationWrapper<UserAccountDto>(data: _userAccountDto, ruleSet: "SsoRegistration", validator: _userAccountDtoValidator);
            var result = validationWrapper.ExecuteValidator();
            if (!result.Data)
            {
                return result;
            }

            result = _userValidator.CheckIfUserExists(_userAccountDto.Username);
            if (!result.Data)
            {
                result.Error = "Username is already used.";
            }

            return result;
        }
    }
}
