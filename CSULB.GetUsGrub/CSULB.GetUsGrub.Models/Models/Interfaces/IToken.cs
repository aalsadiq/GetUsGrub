using System;

namespace CSULB.GetUsGrub.Models
{
    public interface IToken
    {
        string TokenHeader { get; set; }
        string TokenSignature { get; set; }
        DateTime IssuedOn { get; set; }
        DateTime ExpiresOn { get; set; }
    }
}
