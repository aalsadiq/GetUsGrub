using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.UserAccessControl;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public class CreateUserManager : ICreateUserManager<IRegisterUserDto>, ICreateNewUser<IRegisterUserDto>
    {
        public ResponseDto<IRegisterUserDto> CheckUserDoesNotExist(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};

            // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
            using (var gateway = new UserGateway())
            {
                var getUserResult = gateway.GetUserByUsername(responseDto.Data.UserAccount.Username);
                if (getUserResult == null)
                {
                    return responseDto;
                }
                else
                {
                    // TODO: Is this the correct error message?
                    responseDto.Error = "Username is already used.";
                    return responseDto;
                }
            }
        }

        public ResponseDto<IRegisterUserDto> HashPassword(IRegisterUserDto registerUserDto)
        {
            using (var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto})
            {
                responseDto.Data.PasswordSalt = new PasswordSalt();

                // TODO: Why did I pick 128 as my Salt size?
                var salt = PayloadHasher.CreateRandomSalt(128);
                System.Diagnostics.Debug.WriteLine(responseDto.Data.UserAccount.Password);
                var passwordHash = PayloadHasher.HashWithSalt(salt, responseDto.Data.UserAccount.Password);
                if (registerUserDto.UserAccount.Password != null &&
                    registerUserDto.UserAccount.Password != passwordHash)
                {
                    responseDto.Data.UserAccount.Password = passwordHash;
                    responseDto.Data.PasswordSalt.Salt = salt;
                    return responseDto;
                }
                else
                {
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = ErrorHandler.SetGeneralError();
                    return responseDto;
                }
            }
        }

        public ResponseDto<IRegisterUserDto> HashSecurityAnswers(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};
            responseDto.Data.SecurityAnswerSalts = new List<SecurityAnswerSalt>();

            foreach (var securityQuestion in responseDto.Data.SecurityQuestions)
            {
                // TODO: Why did I pick 128 as my Salt size?
                var salt = PayloadHasher.CreateRandomSalt(128);
                var securityAnswerHash = PayloadHasher.HashWithSalt(salt, securityQuestion.QuestionAnswer);
                if (responseDto.Data.UserAccount.Password != null &&
                    responseDto.Data.UserAccount.Password != securityAnswerHash)
                {
                    securityQuestion.QuestionAnswer = securityAnswerHash;
                    responseDto.Data.SecurityAnswerSalts.Add(new SecurityAnswerSalt {Salt = salt});
                }
                else
                {
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = ErrorHandler.SetGeneralError();
                    return responseDto;
                }
            }

            return responseDto;
        }

        public ResponseDto<IRegisterUserDto> CreateClaims(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};

            var claimsFactory = new ClaimsFactory();

            var claims = claimsFactory.CreateIndividualClaims();
            if (claims != null)
            {
                responseDto.Data.Claims = claims;
                return responseDto;
            }
            else
            {
                // TODO: Should this be the general error? Can I extend it so everyone can use it?
                responseDto.Error = ErrorHandler.SetGeneralError();
                return responseDto;
            }
        }

        public ResponseDto<IRegisterUserDto> SetAccountIsActive(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};
            responseDto.Data.UserAccount.IsActive = true;
            return responseDto;
        }

        // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
        public ResponseDto<IRegisterUserDto> CreateNewUser(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = registerUserDto};

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
                foreach (var securityQuestion in registerUserDto.SecurityQuestions)
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
                for (var i=0; i<registerUserDto.SecurityAnswerSalts.Count; i++)
                {
                    result = gateway.StoreSecurityAnswerSalt(responseDto.Data.UserAccount.Username, responseDto.Data.SecurityQuestions[i].QuestionType, responseDto.Data.SecurityAnswerSalts[i].Salt);
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

                // TODO: CreateUserProfile
                // TODO: CreateRestaurantProfile

                return responseDto;
            }
        }
    }
}