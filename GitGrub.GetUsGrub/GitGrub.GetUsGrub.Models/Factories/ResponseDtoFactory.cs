namespace GitGrub.GetUsGrub.Models
{
    public class ResponseDtoFactory
    {
        public static ResponseDto<T> Create<T>(T typeInput)
        {
            return new ResponseDto<T>(typeInput);
        }
    }
}
