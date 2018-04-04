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
        // Automatic Properties
        [Key]
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsFirstTimeUser { get; set; }
        public string RoleType { get; set; }

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual PasswordSalt PasswordSalt { get; set; }
        public virtual AuthenticationToken AuthenticationToken { get; set; }
        public virtual ICollection<SecurityQuestion> SecurityQuestions { get; set; }
        public virtual UserClaims UserClaims { get; set; }

        // Constructors
        public UserAccount() { }

        public UserAccount(string username)
        {
            Username = username;
        }

        public UserAccount(string username, string password, bool isActive, bool isFirstTimeUser, string roleType)
        {
            Username = username;
            Password = password;
            IsActive = isActive;
            IsFirstTimeUser = isFirstTimeUser;
            RoleType = roleType;
        }
    }
}