using System;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public Exception Error { get; set; }
    }
}
