namespace GitGrub.GetUsGrub
{
    public interface ICreateRestaurantUserManager
    {
        ResponseDto<RegisterRestaurantUserDto> CheckUserDoesNotExist(ResponseDto<RegisterRestaurantUserDto> responseDto);
        ResponseDto<RegisterRestaurantUserDto> HashPassword(ResponseDto<RegisterRestaurantUserDto> responseDto);
        ResponseDto<RegisterRestaurantUserDto> CreateClaims(ResponseDto<RegisterRestaurantUserDto> responseDto);
        ResponseDto<RegisterRestaurantUserDto> SetAccountIsActive(ResponseDto<RegisterRestaurantUserDto> responseDto);
        ResponseDto<RegisterRestaurantUserDto> CreateNewUser(ResponseDto<RegisterRestaurantUserDto> responseDto);
    }
}