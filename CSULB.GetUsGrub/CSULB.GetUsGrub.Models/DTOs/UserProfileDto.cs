namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// User Profile DTO
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>
    public class UserProfileDto : IUserProfile
    {
        public string DisplayName { get; set; }

        public string DisplayPicture { get; set; }
    }
}
