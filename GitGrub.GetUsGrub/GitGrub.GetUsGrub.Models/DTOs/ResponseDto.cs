using System;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    /// <summary>
    /// DTO representing any response
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    /// <typeparam name="T">Any input type or DTO</typeparam>
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public Exception Error { get; set; }
    }
}
