using GitGrub.GetUsGrub.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace GitGrub.GetUsGrub
{
    public class RegisterRestaurantUserDto : IRestaurantUserAccount, IBusinessHoursList, ILocation, ISalt
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
        public IEnumerable<BusinessHours> BusinessHoursList { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public IEnumerable<Location> Address { get; set; }

        [Required]
        public IEnumerable<SecurityQuestion> SecurityQuestionsList { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
