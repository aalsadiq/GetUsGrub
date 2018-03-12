using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SecurityQuestion</c> class.
    /// Defines properties pertaining to security question with corresponding answer.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.SecurityQuestion")]
    public class SecurityQuestion : IEntity
    {
        [Key]
        public int? Id { get; set; }
        [ForeignKey("UserAccount")]
        public int? UserId { get; set; }
        public int Question { get; set; }
        public string Answer { get; set; }
        // Navigation Properties
        public virtual UserAccount UserAccount { get; set; }
    }
}
