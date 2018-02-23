using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub
{
    public class CreateRestaurantUserManager : ICreateRestaurantUserManager
    {
        public ResponseDto<RegisterRestaurantUserDto> CheckIfUserExists(ResponseDto<RegisterRestaurantUserDto> registerRestaurantUserResponseDto)
        {
            try
            {
                using (var gateway = new UserGateway())
                {
                    var getUserResult = gateway.GetUserByUsername(registerRestaurantUserResponseDto.Data.Username);

                    if (registerRestaurantUserResponseDto.Data.Username != getUserResult.Username)
                    {
                        return registerRestaurantUserResponseDto;
                    }
                    else
                    {
                        var responseDtoError = ErrorHandler<RegisterRestaurantUserDto>.SetUserExistsError(registerRestaurantUserResponseDto);
                        return responseDtoError;
                    }
                }
            }
            catch
            {
                var responseDtoError = ErrorHandler<RegisterRestaurantUserDto>.SetGeneralError(registerRestaurantUserResponseDto);
                return responseDtoError;
            }
        }

        public ResponseDto<RegisterRestaurantUserDto> HashPassword(ResponseDto<RegisterRestaurantUserDto> registerRestaurantUserResponseDto, ISalt salt)
        {
            try
            {
                var randomSalt = PayloadHasher.CreateRandomSalt(128);
                salt.Salt = randomSalt;
                var passwordHash = PayloadHasher.HashPayload(salt.Salt, registerRestaurantUserResponseDto.Data.Password);
                if (registerRestaurantUserResponseDto.Data.Password != null &&
                    registerRestaurantUserResponseDto.Data.Password != passwordHash)
                {
                    registerRestaurantUserResponseDto.Data.Password = passwordHash;
                    return registerRestaurantUserResponseDto;
                }
                else
                {
                    var responseDtoError = ErrorHandler<RegisterRestaurantUserDto>.SetGeneralError(registerRestaurantUserResponseDto);
                    return responseDtoError;
                }
            }
            catch
            {
                var responseDtoError = ErrorHandler<RegisterRestaurantUserDto>.SetGeneralError(registerRestaurantUserResponseDto);
                return responseDtoError;
            }
        }

        public ResponseDto<RegisterRestaurantUserDto> CreateNewUser(ResponseDto<RegisterRestaurantUserDto> registerRestaurantUserResponseDto)
        {
            try
            {
                using (var gateway = new RestaurantUserGateway())
                {
                    var storeUserResult = gateway.StoreUser(registerRestaurantUserResponseDto.Data);
                    if (storeUserResult)
                    {
                        return registerRestaurantUserResponseDto;
                    }
                    else
                    {
                        var responseDtoError = ErrorHandler<RegisterRestaurantUserDto>.SetGeneralError(registerRestaurantUserResponseDto);
                        return responseDtoError;
                    }
                }
            }
            catch
            {
                var responseDtoError = ErrorHandler<RegisterRestaurantUserDto>.SetGeneralError(registerRestaurantUserResponseDto);
                return responseDtoError;
            }
        }

        public ResponseDto<RegisterRestaurantUserDto> CreateClaims(ResponseDto<RegisterRestaurantUserDto> registerRestaurantUserResponseDto)
        {
            // Based on Account Type
            try
            {
                return registerRestaurantUserResponseDto;
            }
            catch
            {
                var responseDtoError = ErrorHandler<RegisterRestaurantUserDto>.SetGeneralError(registerRestaurantUserResponseDto);
                return responseDtoError;
            }
        }
    }
}