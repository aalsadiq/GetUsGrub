using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>PasswordSalt</c> class.
    /// Defines a property of a salt for a password.
    /// <para>
    /// @author: Jennifer Nguyen, Brian Fann
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.PasswordSalt")]
    public class PasswordSalt : IEntity
    {
        public PasswordSalt()
        {
            Salt = "";
        }
        public PasswordSalt(string salt)
        {
            Salt = salt;
        }

        [ForeignKey("UserAccount")]
        public int? Id { get; set; }
        public string Salt { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
    }
}
