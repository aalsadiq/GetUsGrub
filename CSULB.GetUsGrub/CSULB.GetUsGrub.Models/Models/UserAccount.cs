using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserAccount</c> class.
    /// Defines properties pertaining to a user's account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.UserAccount")]
    public class UserAccount : IUserAccount, IEntity
    {
        [Key]
        public int? Id { get; set; }
        public string Username { get; set; }

        // Stored as a hash
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsFirstTimeUser { get; set; }

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual PasswordSalt PasswordSalt { get; set; }
        public virtual AuthenticationToken Token { get; set; }
        public virtual ICollection<SecurityQuestion> SecurityQuestions { get; set; }
        public virtual UserClaims Claims { get; set; }
    }
}