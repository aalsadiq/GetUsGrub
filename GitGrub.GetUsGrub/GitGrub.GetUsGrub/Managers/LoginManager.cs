using GitGrub.GetUsGrub.DataAccess.Gateways;
using GitGrub.GetUsGrub.Helpers;
using GitGrub.GetUsGrub.Interfaces;
using GitGrub.GetUsGrub.Models.DTOs;
using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Managers
{
    public class LoginManager
    {
        public ResponseDto<LoginDto> CheckIfUserExists(ResponseDto<LoginDto> loginResponseDto)
        {
            try
            {
                using (var gateway = new LoginGateway())
                {
                    var getUserResult = gateway.GetUserByUsername(loginResponseDto.Data.Username);
                    // getUserResult.Username
                    if (loginResponseDto.Data.Username == getUserResult.Username)
                    {
                        loginResponseDto.Data.Password = getUserResult.Password
                    }
                    else
                    {
                        //var responseDtoError = ErrorHandler<RegisterUserDto>.SetUserExistsError(loginResponseDto);
                        var responseDtoError = ErrorHandler<LoginDto>.SetCustomError(loginResponseDto, "CreateUserManager - CheckIfUserExists");
                        return responseDtoError;
                    }
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(loginResponseDto);
                var responseDtoError = ErrorHandler<LoginDto>.SetCustomError(loginResponseDto, "CreateUserManager - CheckIfUserExists - Catch");
                return responseDtoError;
            }
        }
    }
}