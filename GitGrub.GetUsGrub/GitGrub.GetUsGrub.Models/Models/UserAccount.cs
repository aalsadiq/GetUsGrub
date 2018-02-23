using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    [Validator(typeof(UserAccountValidator))]
    [Table("UserAccount")]
    public class UserAccount : IUserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string DisplayName { get; set; }


        // Stored as a hash
        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
