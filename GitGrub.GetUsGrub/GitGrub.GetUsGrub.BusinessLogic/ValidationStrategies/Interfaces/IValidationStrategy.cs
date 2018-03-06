using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The IValidationStrategy.
    /// Defines a method to run validations in a validation strategy.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public interface IValidationStrategy<T>
    {
        ResponseDto<T> RunValidators(T modelToValidate);
    }
}
