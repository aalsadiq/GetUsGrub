using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SecurityQuestion</c> class.
    /// Defines properties pertaining to security question with corresponding answer.
    /// <para>
    /// @author: Brian Fann
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.SecurityQuestion")]
    public class SecurityQuestion : ISecurityQuestion, IEntity
    {
        // Automatic Properties
        [Key]
        public int? Id { get; set; }
        [ForeignKey("UserAccount")]
        public int? UserId { get; set; }
        public int Question { get; set; }
        public string Answer { get; set; }

        // Navigation Properties
        public virtual UserAccount UserAccount { get; set; }
        public virtual SecurityAnswerSalt SecurityAnswerSalt { get; set; }

        // Constructors
        public SecurityQuestion() { }

        public SecurityQuestion(int question, string answer)
        {
            Question = question;
            Answer = answer;
        }
    }
}
