using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public interface IValidationWrapper
    {
        ResponseDto<bool> ExecuteValidator();
    }
}