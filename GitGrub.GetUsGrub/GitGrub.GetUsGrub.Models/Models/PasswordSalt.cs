using GitGrub.GetUsGrub.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models.Models
{
    public class PasswordSalt : ISalt
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAccount")]
        public int UserId { get; set; }
        public string Salt { get; set; }
    }
}
