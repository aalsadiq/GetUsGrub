using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.UserAccessControl;

namespace GitGrub.GetUsGrub
{
    public class CreateRestaurantUserManager : ICreateRestaurantUserManager
    {
        public ResponseDto<RegisterRestaurantUserDto> CheckUserDoesNotExist(ResponseDto<RegisterRestaurantUserDto> registerRestaurantUserResponseDto)
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

        public ResponseDto<RegisterRestaurantUserDto> HashPassword(ResponseDto<RegisterRestaurantUserDto> registerRestaurantUserResponseDto)
        {
            try
            {
                registerRestaurantUserResponseDto.Data.Salt = PayloadHasher.CreateRandomSalt(128);
                var passwordHash = PayloadHasher.HashWithSalt(registerRestaurantUserResponseDto.Data.Salt, registerRestaurantUserResponseDto.Data.Password);
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

        public ResponseDto<RegisterRestaurantUserDto> CreateClaims(ResponseDto<RegisterRestaurantUserDto> registerRestaurantUserResponseDto)
        {
            try
            {
                var claimsFactory = new ClaimsFactory();

                registerRestaurantUserResponseDto.Data.Claims = claimsFactory.CreateIndividualClaims();
                if (registerRestaurantUserResponseDto.Data.Claims != null)
                {
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

        public ResponseDto<RegisterRestaurantUserDto> SetAccountIsActive(ResponseDto<RegisterRestaurantUserDto> registerRestaurantUserResponseDto)
        {
            try
            {
                registerRestaurantUserResponseDto.Data.IsActive = true;
                if (registerRestaurantUserResponseDto.Data.IsActive)
                {
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
    }
}