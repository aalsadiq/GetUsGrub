using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public interface ICreateUserManager
    {
        ResponseDto<IRegisterUserDto> CheckUserDoesNotExist(IRegisterUserDto registerUserDto);
        ResponseDto<IRegisterUserDto> HashPassword(IRegisterUserDto registerUserDto);
        ResponseDto<IRegisterUserDto> CreateClaims(IRegisterUserDto registerUserDto);
        ResponseDto<IRegisterUserDto> SetAccountIsActive(IRegisterUserDto registerUserDto);
    }
}
