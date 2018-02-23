using GitGrub.GetUsGrub.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace GitGrub.GetUsGrub
{
    public class RegisterUserDto : IUserAccount, ISecurityQuestionsList
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string DisplayName { get; set; }

        // Stored as a hash
        [Required]
        public string Password { get; set; }

        public string AccountType { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public IEnumerable<ISecurityQuestion> SecurityQuestionsList { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}