using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
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
