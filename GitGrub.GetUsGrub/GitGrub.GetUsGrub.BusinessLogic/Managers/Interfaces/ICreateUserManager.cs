using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public interface ICreateUserManager<T>
    {
        ResponseDto<T> CheckUserDoesNotExist(T userDto);
        ResponseDto<T> HashPassword(T userDto);
        ResponseDto<T> HashSecurityAnswers(T userDto);
        ResponseDto<T> CreateClaims(T userDto);
        ResponseDto<T> SetAccountIsActive(T userDto);
        ResponseDto<T> CreateNewUser(T userDto);
    }
}