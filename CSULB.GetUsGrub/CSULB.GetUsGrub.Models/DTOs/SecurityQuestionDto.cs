using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SecurityQuestionDto</c> class.
    /// Defines properties pertaining to a data transfer object of a security question.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionDto
    {
        [Required]
        public int Question { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
