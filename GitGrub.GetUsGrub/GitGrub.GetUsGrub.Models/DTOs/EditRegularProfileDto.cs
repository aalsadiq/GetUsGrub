using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    public class EditRegularProfileDto : IProfile
    {
        public string Username { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
