using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    [Table("PasswordSalt")]
    public class PasswordSalt : ISalt
    {
        public int Id { get; set; }

        [Key]
        [ForeignKey("UserAccount")]
        public int Username { get; set; }

        public string Salt { get; set; }
    }
}
