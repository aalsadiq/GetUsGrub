using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Class that will be called in edit user controller.
    /// These are items a user can edit.
    /// </summary>
    public class EditUserDto
    {
        [Required]
        public string Username { get; set; }//The username that will be edited.
        public string NewUsername { get; set; }//Part of user account
        public string NewDisplayName { get; set; }//Part of user account
        public string NewPassword { get; set; }//Part of user account
        public string NewDisplayPicture { get; set; }
        public IList<BusinessHour> NewBusinessHours { get; set; }
        public string NewPhoneNumber { get; set; }
        public Address NewAddress { get; set; }
        public double NewLongitude { get; set; }
        public double NewLatitude { get; set; }
    }
}
