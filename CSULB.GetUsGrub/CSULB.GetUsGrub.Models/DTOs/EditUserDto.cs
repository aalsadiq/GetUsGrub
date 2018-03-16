using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models.DTOs
{
    /// <summary>
    /// Class that will be called in edit user controller.
    /// These are items a user can edit.
    /// </summary>
    public class EditUserDto
    {
        [Required]
        public string Username { get; set; }
        public string NewUsername { get; set; }
        public string NewDisplayName { get; set; }
        public string NewPassword { get; set; }
    }
}
