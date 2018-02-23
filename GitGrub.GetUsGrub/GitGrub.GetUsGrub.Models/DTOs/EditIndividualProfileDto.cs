namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// DTO encapsulating username and editable individual profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/22/18
    /// </summary>
    public class EditIndividualProfileDto : IProfile
    {
        public string Username { get; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }
    }
}
