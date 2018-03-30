using System.Threading.Tasks;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public interface IPasswordValidationServiceAsync
    {
        Task<ResponseDto<bool>> IsPasswordValidAsync(string password);
    }
}
