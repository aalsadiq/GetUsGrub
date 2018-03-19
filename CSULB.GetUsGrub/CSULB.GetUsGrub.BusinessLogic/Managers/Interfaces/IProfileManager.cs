using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Profile manager interface
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProfileManager<T>
    {
        ResponseDto<T> GetProfile(string username);
        ResponseDto<bool> EditProfile(T dto);
    }
}
