using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.UserAccessControl;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public class CreateRestaurantUserManager : ICreateUserManager<RegisterRestaurantUserDto>
    {
        public ResponseDto<RegisterRestaurantUserDto> CheckUserDoesNotExist(RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            var responseDto = new ResponseDto<RegisterRestaurantUserDto> {Data = registerRestaurantUserDto};

            // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
            using (var gateway = new UserGateway())
            {
                var getUserResult = gateway.GetUserByUsername(responseDto.Data.UserAccount.Username);
                if (responseDto.Data.UserAccount.Username != getUserResult.Username)
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

        public ResponseDto<RegisterRestaurantUserDto> HashPassword(RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            var responseDto = new ResponseDto<RegisterRestaurantUserDto> {Data = registerRestaurantUserDto};

            // TODO: Why did I pick 128 as my Salt size?
            var salt = PayloadHasher.CreateRandomSalt(128);
            var passwordHash = PayloadHasher.HashWithSalt(salt, responseDto.Data.UserAccount.Password);
            if (responseDto.Data.UserAccount.Password != null && responseDto.Data.UserAccount.Password != passwordHash)
            {
                responseDto.Data.UserAccount.Password = passwordHash;
                responseDto.Data.PasswordSalt.Salt = salt;
                return responseDto;
            }
            else
            {
                // TODO: Should this be the general error? Can I extend it so everyone can use it?
                responseDto.Error = "Something went wrong. Please try again later.";
                return responseDto;
            }
        }

        public ResponseDto<RegisterRestaurantUserDto> HashSecurityAnswers(RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            var responseDto = new ResponseDto<RegisterRestaurantUserDto> {Data = registerRestaurantUserDto};
            responseDto.Data.SecurityAnswerSalts = new List<SecurityAnswerSalt>();

            for (var i = 0; i < responseDto.Data.SecurityQuestions.Count; i++)
            {
                // TODO: Why did I pick 128 as my Salt size?
                var salt = PayloadHasher.CreateRandomSalt(128);
                var securityAnswerHash = PayloadHasher.HashWithSalt(salt, responseDto.Data.SecurityQuestions[i].QuestionAnswer);
                if (responseDto.Data.SecurityQuestions[i].QuestionAnswer != null &&
                    responseDto.Data.SecurityQuestions[i].QuestionAnswer != securityAnswerHash)
                {
                    responseDto.Data.SecurityQuestions[i].QuestionAnswer = securityAnswerHash;
                    responseDto.Data.SecurityAnswerSalts.Add(new SecurityAnswerSalt { Salt = salt });
                }
                else
                {
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = "Something went wrong. Please try again later.";
                    return responseDto;
                }
            }

            return responseDto;
        }

        public ResponseDto<RegisterRestaurantUserDto> CreateClaims(RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            var responseDto = new ResponseDto<RegisterRestaurantUserDto> {Data = registerRestaurantUserDto};

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
                responseDto.Error = "Something went wrong. Please try again later.";
                return responseDto;
            }
        }

        public ResponseDto<RegisterRestaurantUserDto> SetAccountIsActive(RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            var responseDto = new ResponseDto<RegisterRestaurantUserDto> {Data = registerRestaurantUserDto};
            responseDto.Data.UserAccount.IsActive = true;
            return responseDto;
        }

        // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
        public ResponseDto<RegisterRestaurantUserDto> CreateNewUser(RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            var responseDto = new ResponseDto<RegisterRestaurantUserDto> {Data = registerRestaurantUserDto};

            using (var gateway = new UserGateway())
            {
                var result = gateway.StoreUserAccount(responseDto.Data.UserAccount);
                if (!result)
                {
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = "Something went wrong. Please try again later.";
                    return responseDto;
                }

                result = gateway.StorePasswordSalt(responseDto.Data.UserAccount.Username, responseDto.Data.PasswordSalt.Salt);
                if (!result)
                {
                    // TODO: Ask Brian to delete previous UserAccount
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = "Something went wrong. Please try again later.";
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
                        responseDto.Error = "Something went wrong. Please try again later.";
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
                        responseDto.Error = "Something went wrong. Please try again later.";
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
                    responseDto.Error = "Something went wrong. Please try again later.";
                    return responseDto;
                }

                result = gateway.StoreRestaurantAccount(responseDto.Data.RestaurantAccount);
                if (!result)
                {
                    // TODO: Ask Brian to delete previous UserAccount
                    // TODO: Ask Brian to delete previous UserAccount
                    // TODO: Ask Brian to delete previous PasswordSalt
                    // TODO: Ask Brian to delete previous SecurityQuestions
                    // TODO: Ask Brian to delete previous Claims
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = "Something went wrong. Please try again later.";
                    return responseDto;
                }

                return responseDto;
            }
        }
    }
}
