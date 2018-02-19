using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models
{
    public class RegularProfile : IProfile
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }

        // Constructor
        public RegularProfile(string name, string picture)
        {
            ProfileName = name;
            ProfilePicture = picture;
        }
    }
}