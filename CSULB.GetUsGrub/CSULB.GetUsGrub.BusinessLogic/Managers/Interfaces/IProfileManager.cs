using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public interface IProfileManager<T>
    {
        ResponseDto<T> GetProfile(string username);
        ResponseDto<bool> EditProfile(T dto);
    }
}
