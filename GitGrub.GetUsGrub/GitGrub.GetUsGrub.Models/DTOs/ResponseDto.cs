using System;

namespace GitGrub.GetUsGrub
{
    public class ResponseDto<T>
    {
        // A type of data you would like to store in the ResponseDto
        public T Data { get; set; }
        
        public Exception Error { get; set; }

        public string ValidationErrors { get; set; }

        // Constructor of ResponseDto
        public ResponseDto(T data)
        {
            Data = data;
            Error = null;
            ValidationErrors = null;
        }
    }
}
