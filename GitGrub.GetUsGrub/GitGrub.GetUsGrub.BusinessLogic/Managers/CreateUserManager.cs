using GitGrub.GetUsGrub.BusinessLogic.Managers.Interfaces;
using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.UserAccessControl;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    {
        public ResponseDto<IRegisterUserDto> CheckUserDoesNotExist(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

            // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
            using (var gateway = new UserGateway())
            {
                var getUserResult = gateway.GetUserByUsername(registerUserDto.Username);
                if (registerUserDto.Username != getUserResult.Username)
                {
                    responseDto.Data = registerUserDto;
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
            var responseDto = new ResponseDto<IRegisterUserDto>();

            // TODO: Why did I pick 128 as my Salt size?
            registerUserDto.Salt = PayloadHasher.CreateRandomSalt(128);
            var passwordHash = PayloadHasher.HashWithSalt(registerUserDto.Salt, registerUserDto.Password);
            if (registerUserDto.Password != null && registerUserDto.Password != passwordHash)
            {
                registerUserDto.Password = passwordHash;
                responseDto.Data = registerUserDto;
                return responseDto;
            }
            else
            {
                // TODO: Should this be the general error? Can I extend it so everyone can use it?
                responseDto.Error = "Something went wrong. Please try again later.";
                return responseDto;
            }
        }

        public ResponseDto<IRegisterUserDto> CreateClaims(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

            var claimsFactory = new ClaimsFactory();

            registerUserDto.Claims = claimsFactory.CreateIndividualClaims();
            if (registerUserDto.Claims != null)
            {
                responseDto.Data = registerUserDto;
                return responseDto;
            }
            else
            {
                // TODO: Should this be the general error? Can I extend it so everyone can use it?
                responseDto.Error = "Something went wrong. Please try again later.";
                return responseDto;
            }
        }

        public ResponseDto<IRegisterUserDto> SetAccountIsActive(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

            registerUserDto.IsActive = true;
            if (registerUserDto.IsActive)
            {
                responseDto.Data = registerUserDto;
                return responseDto;
            }
            else
            {
                // TODO: Should this be the general error? Can I extend it so everyone can use it?
                responseDto.Error = "Something went wrong. Please try again later.";
                return responseDto;
            }

        }

        // TODO: Confirm with Brian his UserGateway with what is being returned and error handling
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

            using (var gateway = new UserGateway())
            {
                var storeUserResult = gateway.StoreUserAccount(registerUserDto);
                if (storeUserResult)
                {
                    responseDto.Data = registerUserDto;
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