using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models.Models
{
    [Table("Tokens")]
    public class TokenModel
    {
        [Key]
        public int TokenId { get; set; }
        public string TokenHeadder { get; set; }

        [ForeignKey("UserAccount")]
        public string Username { get; set; }
        public string TokenSignature { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
