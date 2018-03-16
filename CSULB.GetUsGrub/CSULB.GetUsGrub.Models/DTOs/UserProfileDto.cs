namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// User Profile DTO
    /// @author: Andrew Kao
    /// @updated: 3/15/18
    /// </summary>
    public class UserProfileDto : IUserProfile
    {
        public string Username { get; set; }

        public string DisplayName { get; set; }

        public string DisplayPicture { get; set; }


        // Constructor
        public UserProfileDto(string username, string displayName, string displayPicture)
        {
            Username = username;
            DisplayName = displayName;
            DisplayPicture = displayPicture;
        }
    }
}
