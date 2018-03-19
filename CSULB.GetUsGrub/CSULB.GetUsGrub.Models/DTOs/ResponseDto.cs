namespace CSULB.GetUsGrub.Models
{
    public class ResponseDto<T>
    {
        // A type of data you would like to store in the ResponseDto
        public T Data { get; set; }
        public string Error { get; set; }
    }
}