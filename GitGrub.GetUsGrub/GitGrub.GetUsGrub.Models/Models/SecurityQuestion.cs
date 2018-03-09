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
    public class SecurityQuestion
    {
        [Required]
        public int Question { get; set; }

        // QuestionAnswer is stored as hash
        [Required]
        public string Answer { get; set; }
    }
}
