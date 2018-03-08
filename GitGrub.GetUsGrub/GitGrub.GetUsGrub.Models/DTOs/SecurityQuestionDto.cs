using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SecurityQuestion</c> class.
    /// Defines properties pertaining to security question with corresponding answer.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionDto : ISecurityQuestion
    {
        [Required]
        public int QuestionType { get; set; }

        // QuestionAnswer is stored as hash
        [Required]
        public string QuestionAnswer { get; set; }
    }
}
