using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    public class RegisterUserDto : IRegisterUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public string Salt { get; set; }

        [Required]
        public IEnumerable<SecurityQuestion> SecurityQuestions { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}