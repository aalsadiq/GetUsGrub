using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub
{
    public interface ICreateUserManager
    {
        ResponseDto<RegisterUserDto> CheckIfUserExists(ResponseDto<RegisterUserDto> responseDto);
        ResponseDto<RegisterUserDto> HashPassword(ResponseDto<RegisterUserDto> responseDto, ISalt salt);
        ResponseDto<RegisterUserDto> CreateNewUser(ResponseDto<RegisterUserDto> responseDto);
    }
}
