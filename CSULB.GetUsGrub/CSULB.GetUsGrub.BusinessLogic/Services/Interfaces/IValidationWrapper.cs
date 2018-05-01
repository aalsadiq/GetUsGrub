using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The IValidationWrapper interface.
    /// An interface used to abstract the Validation Wrapper class for the Command Design Pattern.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/17/2018
    /// </para>
    /// </summary>
    public interface IValidationWrapper
    {
        ResponseDto<bool> ExecuteValidator();
    }
}