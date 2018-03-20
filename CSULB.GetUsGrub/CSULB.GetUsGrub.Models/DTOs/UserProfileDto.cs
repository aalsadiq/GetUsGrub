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


        // Constructors
        public UserProfileDto(string displayName, string displayPicture)
        {
            DisplayName = displayName;
            DisplayPicture = displayPicture;
        }

        public UserProfileDto(UserProfile userProfile)
        {
            DisplayName = userProfile.DisplayName;
            DisplayPicture = userProfile.DisplayPicture;
        }
    }
}
