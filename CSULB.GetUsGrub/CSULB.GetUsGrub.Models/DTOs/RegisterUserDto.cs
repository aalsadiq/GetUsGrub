using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RegisterUserDto</c> class.
    /// Defines properties pertaining to a data transfer object for registering a user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class RegisterUserDto
    {
        [Required]
        public UserAccountDto UserAccountDto { get; set; }
        [Required]
        public IList<SecurityQuestionDto> SecurityQuestionDtos { get; set; }
        [Required]
        public UserProfileDto UserProfileDto { get; set; }
    }
}
