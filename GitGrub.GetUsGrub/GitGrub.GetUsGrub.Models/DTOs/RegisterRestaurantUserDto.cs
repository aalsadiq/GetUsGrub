using GitGrub.GetUsGrub.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub
{
    public class RegisterRestaurantUserDto : IRestaurantUserAccount, IBusinessHoursList, ILocation
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
        public IEnumerable<BusinessHours> BusinessHoursList { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public IEnumerable<Location> Address { get; set; }
    }
}
