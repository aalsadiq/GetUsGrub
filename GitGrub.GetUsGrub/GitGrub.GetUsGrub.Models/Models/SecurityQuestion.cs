using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    // TODO: Do you still need to declare this table?
    [Table("SecurityQuestion")]
    public class SecurityQuestion : ISecurityQuestion
    {
        //TODO: Do you still need this key?
        [Key]
        public int Id { get; set; }

        //TODO: Do you still need this Foreign Key?
        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        [Required]
        public string QuestionType { get; set; }

        [Required]
        public string QuestionAnswer { get; set; }
    }
}
