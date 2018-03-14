using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Add data annotations? [-Jenn]
    /// <summary>
    /// The <c>UserAccount</c> class.
    /// Defines properties pertaining to a user account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserAccount : IUserAccount
    {
        [Key]
        public int? Id { get; set; }
        public string Username { get; set; }

        // Stored as a hash
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool FirstTimeUser { get; set; }

        public virtual PasswordSalt PasswordSalt { get; set; }
    }
}
