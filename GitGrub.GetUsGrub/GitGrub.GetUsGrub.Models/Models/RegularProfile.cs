using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Models.Models
{
    /// <summary>
    /// Regular user profile class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public class RegularProfile : IProfile
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
    }
}