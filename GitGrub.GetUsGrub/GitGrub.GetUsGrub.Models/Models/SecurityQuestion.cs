using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    public class SecurityQuestion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required security question")]
        public string QuestionType { get; set; }

        [Required(ErrorMessage = "Required security question answer")]
        public string QuestionAnswer { get; set; }

        // Navigation Property
        public UserAccount UserAccount { get; set; }
    }
}
