using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    [Validator(typeof(UserAccountValidator))]
    // TODO: Do we still need to declare table?
    [Table("UserAccount")]
    public class UserAccount : IUserAccount
    {
        // TODO: Do we still need to delcare key?
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string DisplayName { get; set; }

        // Stored as a hash
        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
