using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models
{
    public class RegularProfile : IProfile
    {
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
    }
}