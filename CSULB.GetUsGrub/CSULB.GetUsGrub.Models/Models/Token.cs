using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    [Table("GetUsGrub.Token")]
    public class Token : IToken, IEntity
    {
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }
        public string TokenHeader { get; set; }
        public string TokenSignature { get; set; }
        public string Salt { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        // Navigation Properties 
        public virtual UserAccount UserAccount { get; set; }
    }
}