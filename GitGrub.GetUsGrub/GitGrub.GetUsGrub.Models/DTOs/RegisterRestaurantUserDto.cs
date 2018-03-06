using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RegisterRestaurantUserDto</c>.
    /// Defines properties pertaining to restaurant registration.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class RegisterRestaurantUserDto : IRegisterRestaurantUserDto
    {
        [Required]
        public UserAccount UserAccount { get; set; }

        public PasswordSalt PasswordSalt { get; set; }

        [Required]
        public IList<SecurityQuestion> SecurityQuestions { get; set; }

        public IList<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }

        public Claims Claims { get; set; }

        [Required]
        public RestaurantAccount RestaurantAccount { get; set; }
    }
}
