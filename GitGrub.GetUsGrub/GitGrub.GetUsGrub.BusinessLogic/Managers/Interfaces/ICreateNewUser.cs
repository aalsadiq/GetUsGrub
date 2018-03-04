using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic.Managers.Interfaces
{
    public interface ICreateNewUser<T>
    {
        ResponseDto<T> CreateNewUser(T user);
    }
}
