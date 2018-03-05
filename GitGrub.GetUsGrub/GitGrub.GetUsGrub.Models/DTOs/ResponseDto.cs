using System;

namespace GitGrub.GetUsGrub.Models
{
    public class ResponseDto<T> : IDisposable
    {
        // A type of data you would like to store in the ResponseDto
        public T Data { get; set; }

        public string Error { get; set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
