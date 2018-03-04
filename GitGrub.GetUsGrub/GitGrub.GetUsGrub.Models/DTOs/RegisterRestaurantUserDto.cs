using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    public class RegisterRestaurantUserDto : IRegisterRestaurantUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string DisplayName { get; set; }

        // Stored as a hash
        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string Salt { get; set; }

        [Required]
        public IEnumerable<BusinessHour> BusinessHours { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public IEnumerable<SecurityQuestion> SecurityQuestions { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
