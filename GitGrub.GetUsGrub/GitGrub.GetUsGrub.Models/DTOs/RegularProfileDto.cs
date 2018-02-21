using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    public class RegularProfileDto : IProfile
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
