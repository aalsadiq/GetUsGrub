using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    class AuthenticationTokenDTO : ISalt
    {
        public string TokenHeader { get; set; }
        public string TokenPayload { get; set; }
        public string TokenSignature { get; set; }
        public string Salt { get; set; }
    }
}
