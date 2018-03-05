using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public interface ICreateNewUser<T>
    {
        ResponseDto<T> CreateNewUser(T user);
    }
}
