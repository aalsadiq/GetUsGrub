using GitGrub.GetUsGrub.BusinessLogic.Managers.Interfaces;
using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.UserAccessControl;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public class CreateUserManager : ICreateUserManager, ICreateNewUser<IRegisterUserDto>
    {
        public ResponseDto<IRegisterUserDto> CheckUserDoesNotExist(string username)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

            // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
            using (var gateway = new UserGateway())
            {
                var getUserResult = gateway.GetUserByUsername(username);
                if (username != getUserResult.Username)
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

        public ResponseDto<IRegisterUserDto> HashPassword(string password)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

            // TODO: Why did I pick 128 as my Salt size?
            var salt = PayloadHasher.CreateRandomSalt(128);
            var passwordHash = PayloadHasher.HashWithSalt(salt, password);
            if (password != null && password != passwordHash)
            {
                responseDto.Data.Password = passwordHash;
                responseDto.Data.Salt = salt;
                return responseDto;
            }
            else
            {
                // TODO: Should this be the general error? Can I extend it so everyone can use it?
                responseDto.Error = "Something went wrong. Please try again later.";
                return responseDto;
            }
        }

        public ResponseDto<IRegisterUserDto> CreateClaims()
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

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

        public ResponseDto<IRegisterUserDto> SetAccountIsActive()
        {
            var responseDto = new ResponseDto<IRegisterUserDto> {Data = {IsActive = true}};
            return responseDto;
        }

        // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
        public ResponseDto<IRegisterUserDto> CreateNewUser(IUserAccount userAccount)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

            using (var gateway = new UserGateway())
            {
                var storeUserResult = gateway.StoreUserAccount(userAccount);
                if (storeUserResult)
                {
                    return responseDto;
                }
                else
                {
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = "Something went wrong. Please try again later.";
                    return responseDto;
                }
            }
        }
    }
}