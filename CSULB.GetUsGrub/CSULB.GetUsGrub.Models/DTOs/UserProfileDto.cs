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
        //TODO: @Angelica - tell group that with research you found out that it can be stored in a byte... 
        public string DisplayPictureUrl { get; set; }
        public string DisplayName { get; set; }
    }
}
