using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// DTO encapsulating individual profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/22/18
    /// </summary>
    public class IndividualProfileDto : IProfile
    {
        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }
    }
}
