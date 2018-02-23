using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub
{
    public interface ICreateRestaurantUserManager
    {
        ResponseDto<RegisterRestaurantUserDto> CheckIfUserExists(ResponseDto<RegisterRestaurantUserDto> responseDto);
        ResponseDto<RegisterRestaurantUserDto> HashPassword(ResponseDto<RegisterRestaurantUserDto> responseDto, ISalt salt);
        ResponseDto<RegisterRestaurantUserDto> CreateNewUser(ResponseDto<RegisterRestaurantUserDto> responseDto);
        ResponseDto<RegisterRestaurantUserDto> CreateClaims(ResponseDto<RegisterRestaurantUserDto> responseDto);
    }
}