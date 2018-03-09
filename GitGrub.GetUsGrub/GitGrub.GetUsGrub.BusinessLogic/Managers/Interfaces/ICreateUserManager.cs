using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The ICreateUserManager interface.
    /// A contract with methods for the CreateUserManager class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public interface ICreateUserManager<T>
    {
        ResponseDto<T> CheckUserDoesNotExist(T userDto);
        ResponseDto<T> HashPassword(T userDto);
        ResponseDto<T> HashSecurityAnswers(T userDto);
        ResponseDto<T> CreateClaims(T userDto);
        ResponseDto<T> SetAccountIsActive(T userDto);
    }
}