using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Add data annotations? [-Jenn]
    /// <summary>
    /// The <c>SecurityAnswerSalt</c> class.
    /// Defines properties pertaining to a salt for a security answer.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class SecurityAnswerSalt
    {
        [Key]
        [ForeignKey("SecurityQuestion")]
        public int? Id { get; set; }
        public string Salt { get; set; }

        // Navigation Properties
        public virtual SecurityQuestion SecurityQuestion { get; set; }
    }
}
