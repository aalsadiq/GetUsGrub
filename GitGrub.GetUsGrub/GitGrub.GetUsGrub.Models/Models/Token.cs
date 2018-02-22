using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    [Table("GetUsGrub.Tokens")]
    public class Token
    {
        [Key]
        public int TokenId { get; set; }
        public string TokenHeader { get; set; }

        [ForeignKey("GetUsGrub.UserAccounts")]
        public int UserId { get; set; }

        public string Username { get; set; }
        public string TokenSignature { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
