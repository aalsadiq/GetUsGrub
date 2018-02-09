using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitGrub.GetUsGrub.Models
{
    public class RegularProfile : IProfile
    {
        private string ProfileName { get; set; }

        private string ProfilePicture { get; set; }

        // Constructor
        public RegularProfile(string profileName, string profilePicture)
        {
            ProfileName = profileName;
            ProfilePicture = profilePicture;
        }

        public string GetProfileName()
        {
            throw new NotImplementedException();
        }

        public string GetProfilePicture()
        {
            throw new NotImplementedException();
        }
    }
}