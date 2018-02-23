using System;

namespace GitGrub.GetUsGrub.Models
{
    public class Token : IToken
    {
        public int TokenId { get; set; }

        public string TokenHeader { get; set; }
        
        public string TokenSignature { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
