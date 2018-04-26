using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>ResetPasswordDto</c> class.
    /// Defines properties pertaining to a data transfer object for resetting a user's password.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/22/2018
    /// </para>
    /// </summary>
    public class ResetPasswordDto
    {
        // Automatic properties
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public IList<SecurityQuestionDto> SecurityQuestionDtos { get; set; }

        // Constructors
        public ResetPasswordDto() { }
        public ResetPasswordDto(string username)
        {
            Username = username;
        }
        public ResetPasswordDto(string username, IList<SecurityQuestionDto> securityQuestionDtos)
        {
            Username = username;
            SecurityQuestionDtos = securityQuestionDtos;
        }
        public ResetPasswordDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
