using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Regular user profile class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public class RegularProfile : IProfile
    {
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
    }
}