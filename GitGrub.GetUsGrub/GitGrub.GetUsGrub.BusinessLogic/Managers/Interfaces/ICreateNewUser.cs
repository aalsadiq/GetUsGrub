using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The ICreateNewUser interface.
    /// Contains the CreateNewUser generic method.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public interface ICreateNewUser<T>
    {
        ResponseDto<T> CreateNewUser(T user);
    }
}
