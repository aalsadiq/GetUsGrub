using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    [Table("SecurityQuestion")]
    public class SecurityQuestion
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        [Required]
        public string QuestionType { get; set; }

        [Required]
        public string QuestionAnswer { get; set; }
    }
}
