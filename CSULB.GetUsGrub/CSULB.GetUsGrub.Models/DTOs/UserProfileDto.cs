namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>IndividualProfileDto</c> class.
    /// Defines properties pertaining to a data transfer object of an individual profile.
    /// <para>
    /// @author: Jennifer Nguyen, Andrew Kao
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserProfileDto : IUserProfile
    {
        // Automatic Properties
        public string Username { get; set; }
        public string DisplayPicture { get; set; }
        public string DisplayName { get; set; }

        //Add path change display to file

        // Constructors
        public UserProfileDto() { }

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
