using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public interface ICreateUserManager
    {
        ResponseDto<IRegisterUserDto> CheckUserDoesNotExist(string username);
        ResponseDto<IRegisterUserDto> HashPassword(string password);
        ResponseDto<IRegisterUserDto> CreateClaims();
        ResponseDto<IRegisterUserDto> SetAccountIsActive();
    }
}
