using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    public class SecurityQuestion : ISecurityQuestion
    {
        [Required]
        public string QuestionType { get; set; }

        [Required]
        public string QuestionAnswer { get; set; }
    }
}
