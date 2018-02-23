namespace GitGrub.GetUsGrub
{
    public interface ICreateUserManager
    {
        ResponseDto<RegisterUserDto> CheckUserDoesNotExist(ResponseDto<RegisterUserDto> responseDto);
        ResponseDto<RegisterUserDto> HashPassword(ResponseDto<RegisterUserDto> responseDto);
        ResponseDto<RegisterUserDto> CreateClaims(ResponseDto<RegisterUserDto> responseDto);
        ResponseDto<RegisterUserDto> SetAccountIsActive(ResponseDto<RegisterUserDto> responseDto);
        ResponseDto<RegisterUserDto> CreateNewUser(ResponseDto<RegisterUserDto> responseDto);
    }
}
