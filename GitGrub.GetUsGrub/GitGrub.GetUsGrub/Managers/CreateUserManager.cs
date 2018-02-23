using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub
{
    public class CreateUserManager : ICreateUserManager
    {
        public ResponseDto<RegisterUserDto> CheckIfUserExists(ResponseDto<RegisterUserDto> registerUserResponseDto)
        {
            try
            {
                using (var gateway = new UserGateway())
                {
                    var getUserResult = gateway.GetUserByUsername(registerUserResponseDto.Data.Username);
                    // getUserResult.Username
                    if (registerUserResponseDto.Data.Username != getUserResult.Username)
                    {
                        return registerUserResponseDto;
                    }
                    else
                    {
                        //var responseDtoError = ErrorHandler<RegisterUserDto>.SetUserExistsError(registerUserResponseDto);
                        var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CheckIfUserExists");
                        return responseDtoError;
                    }
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CheckIfUserExists - Catch");
                return responseDtoError;
            }
        }

        public ResponseDto<RegisterUserDto> HashPassword(ResponseDto<RegisterUserDto> registerUserResponseDto, ISalt salt)
        {
            try
            {
                var randomSalt = PayloadHasher.CreateRandomSalt(128);
                salt.Salt = randomSalt;
                var passwordHash = PayloadHasher.HashPayload(salt.Salt, registerUserResponseDto.Data.Password);
                if (registerUserResponseDto.Data.Password != null &&
                    registerUserResponseDto.Data.Password != passwordHash)
                {
                    registerUserResponseDto.Data.Password = passwordHash;
                    return registerUserResponseDto;
                }
                else
                {
                    //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                    var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - HashPassword");
                    return responseDtoError;
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - HashPassword - Catch");
                return responseDtoError;
            }
        }

        public ResponseDto<RegisterUserDto> CreateNewUser(ResponseDto<RegisterUserDto> registerUserResponseDto)
        {
            try
            {
                using (var gateway = new UserGateway())
                {
                    var storeUserResult = gateway.StoreUser(registerUserResponseDto.Data);
                    if (storeUserResult)
                    {
                        return registerUserResponseDto;
                    }
                    else
                    {
                        //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                        var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateNewUser");
                        return responseDtoError;
                    }
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateNewUser - Catch");
                return responseDtoError;
            }
        }

        public ResponseDto<RegisterUserDto> CreateClaims(ResponseDto<RegisterUserDto> registerUserResponseDto)
        {
            // Based on Account Type
            try
            {
                return registerUserResponseDto;
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateClaims - Catch");
                return responseDtoError;
            }
        }
    }
}