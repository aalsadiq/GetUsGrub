using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for a service that checks passwords against a set of validations.
    /// </summary>
    public interface IPasswordValidationService
    {
        /// <summary>
        /// Checks if password is a strong password according to a set of validations.
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns>True if strong password, false if weak password</returns>
        ResponseDto<bool> IsPasswordValid(string password);
    }
}
