using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.UserAccessControl;

namespace GitGrub.GetUsGrub
{
    public class CreateUserManager : ICreateUserManager
    {
        public ResponseDto<RegisterUserDto> CheckUserDoesNotExist(ResponseDto<RegisterUserDto> registerUserResponseDto)
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
                        var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CheckUserDoesNotExist");
                        return responseDtoError;
                    }
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CheckUserDoesNotExist - Catch");
                return responseDtoError;
            }
        }

        public ResponseDto<RegisterUserDto> HashPassword(ResponseDto<RegisterUserDto> registerUserResponseDto)
        {
            try
            {
                registerUserResponseDto.Data.Salt = PayloadHasher.CreateRandomSalt(128);
                var passwordHash = PayloadHasher.HashWithSalt(registerUserResponseDto.Data.Salt, registerUserResponseDto.Data.Password);
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

        public ResponseDto<RegisterUserDto> CreateClaims(ResponseDto<RegisterUserDto> registerUserResponseDto)
        {
            try
            {
                var claimsFactory = new ClaimsFactory();

                registerUserResponseDto.Data.Claims = claimsFactory.CreateIndividualClaims();
                if (registerUserResponseDto.Data.Claims != null)
                {
                    return registerUserResponseDto;
                }
                else
                {
                    //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                    var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateClaims");
                    return responseDtoError;
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateClaims - Catch");
                return responseDtoError;
            }
        }

        public ResponseDto<RegisterUserDto> SetAccountIsActive(ResponseDto<RegisterUserDto> registerUserResponseDto)
        {
            try
            {
                registerUserResponseDto.Data.IsActive = true;
                if (registerUserResponseDto.Data.IsActive)
                {
                    return registerUserResponseDto;
                }
                else
                {
                    //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                    var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - SetAccountIsActive");
                    return responseDtoError;
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto,
                    "CreateUserManager - SetAccountIsActive - Catch");
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
    }
}