using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    /// <summary>
    /// DTO representing the information required to edit a regular profile
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    public class EditRegularProfileDto : IProfile
    {
        public string Username { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
    }
}
