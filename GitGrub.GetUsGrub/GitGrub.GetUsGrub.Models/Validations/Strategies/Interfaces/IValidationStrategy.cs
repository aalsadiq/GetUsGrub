namespace GitGrub.GetUsGrub.Models
{
    public interface IValidationStrategy<T>
    {
        ResponseDto<T> RunValidators(T modelToValidate);
    }
}
