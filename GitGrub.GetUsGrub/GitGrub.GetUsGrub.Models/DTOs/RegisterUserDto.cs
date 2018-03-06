using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RegisterUserDto</c> class.
    /// Defines properties pertaining to user registration.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class RegisterUserDto : IRegisterUserDto
    {
        [Required]
        public UserAccount UserAccount { get; set; }

        public PasswordSalt PasswordSalt { get; set; }

        [Required]
        public IList<SecurityQuestion> SecurityQuestions { get; set; }

        public IList<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }

        public Claims Claims { get; set; }
    }
}