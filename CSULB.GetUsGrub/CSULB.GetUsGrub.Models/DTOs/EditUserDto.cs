using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Class that will be called in edit user controller.
    /// These are items a user can edit.
    /// </summary>
    public class EditUserDto
    {
        // Automatic Properties
        [Required]
        public string Username { get; set; }//The username that will be edited.
        public string NewUsername { get; set; }//Part of user account
        public string NewDisplayName { get; set; }//Part of user account
    }
}
