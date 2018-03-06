using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public class CreateRestaurantUserManager : ICreateNewUser<IRegisterRestaurantUserDto>
    {
        // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
        public ResponseDto<IRegisterRestaurantUserDto> CreateNewUser(IRegisterRestaurantUserDto registerRestaurantUserDto)
        {
            var responseDto = new ResponseDto<IRegisterRestaurantUserDto> {Data = registerRestaurantUserDto};

            using (var gateway = new UserGateway())
            {
                var result = gateway.StoreUserAccount(responseDto.Data.UserAccount);
                if (!result)
                {
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = ErrorHandler.SetGeneralError();
                    return responseDto;
                }

                result = gateway.StorePasswordSalt(responseDto.Data.UserAccount.Username, responseDto.Data.PasswordSalt.Salt);
                if (!result)
                {
                    // TODO: Ask Brian to delete previous UserAccount
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = ErrorHandler.SetGeneralError();
                    return responseDto;
                }

                foreach (SecurityQuestion securityQuestion in responseDto.Data.SecurityQuestions)
                {
                    result = gateway.StoreSecurityQuestion(responseDto.Data.UserAccount.Username, securityQuestion);
                    if (!result)
                    {
                        // TODO: Ask Brian to delete previous UserAccount
                        // TODO: Ask Brian to delete previous PasswordSalt
                        // TODO: Should this be the general error? Can I extend it so everyone can use it?
                        responseDto.Error = ErrorHandler.SetGeneralError();
                        return responseDto;
                    }
                }

                for (var i = 0; i < responseDto.Data.SecurityAnswerSalts.Count; i++)
                {
                    result = gateway.StoreSecurityAnswerSalt(responseDto.Data.UserAccount.Username, responseDto.Data.SecurityQuestions[i].QuestionType, registerRestaurantUserDto.SecurityAnswerSalts[i].Salt);
                    if (!result)
                    {
                        // TODO: Ask Brian to delete previous UserAccount
                        // TODO: Ask Brian to delete previous PasswordSalt
                        // TODO: Ask Brian to delete previous SecurityQuestions
                        // TODO: Should this be the general error? Can I extend it so everyone can use it?
                        responseDto.Error = ErrorHandler.SetGeneralError();
                        return responseDto;
                    }
                }

                result = gateway.StoreClaims(responseDto.Data.UserAccount.Username, responseDto.Data.Claims);
                if (!result)
                {
                    // TODO: Ask Brian to delete previous UserAccount
                    // TODO: Ask Brian to delete previous UserAccount
                    // TODO: Ask Brian to delete previous PasswordSalt
                    // TODO: Ask Brian to delete previous SecurityQuestions
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = ErrorHandler.SetGeneralError();
                    return responseDto;
                }

                result = gateway.StoreRestaurantAccount(responseDto.Data.UserAccount.Username, responseDto.Data.RestaurantAccount);
                if (!result)
                {
                    // TODO: Ask Brian to delete previous UserAccount
                    // TODO: Ask Brian to delete previous UserAccount
                    // TODO: Ask Brian to delete previous PasswordSalt
                    // TODO: Ask Brian to delete previous SecurityQuestions
                    // TODO: Ask Brian to delete previous Claims
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = ErrorHandler.SetGeneralError();
                    return responseDto;
                }

                return responseDto;
            }
        }
    }
}
