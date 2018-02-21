using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    public class ResponseDto<T>
    {
        T Data { get; set; }
        Exception Error { get; set; }
    }
}
