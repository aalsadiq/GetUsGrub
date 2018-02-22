using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    [Table("GetUsGrub.Tokens")]
    public class TokenSalt : ISalt
    { 
        [Key]
        public int Id { get; set; }

        [ForeignKey("GetUsGrub.Tokens")]
        public int TokenId { get; set; }
        public string Salt { get; set; }
    }
}

