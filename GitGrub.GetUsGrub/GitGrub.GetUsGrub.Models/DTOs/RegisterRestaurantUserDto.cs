using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    public class RegisterRestaurantUserDto : IRegisterRestaurantUserDto
    {
        [Required]
        public UserAccount UserAccount { get; set; }

        public PasswordSalt PasswordSalt { get; set; }

        [Required]
        public IList<SecurityQuestion> SecurityQuestions { get; set; }

        public IList<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }

        public ICollection<Claim> Claims { get; set; }

        [Required]
        public RestaurantAccount RestaurantAccount { get; set; }
    }
}
