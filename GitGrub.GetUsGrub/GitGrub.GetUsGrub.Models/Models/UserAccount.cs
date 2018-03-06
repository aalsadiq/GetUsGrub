using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserAccount</c> class.
    /// Defines properties pertaining to a user account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    [Validator(typeof(UserAccountValidator))]
    public class UserAccount : IUserAccount
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string DisplayName { get; set; }

        // Stored as a hash
        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
