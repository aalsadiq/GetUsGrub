namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Add data annotations? [-Jenn]
    /// <summary>
    /// The <c>UserProfile</c> class.
    /// Defines properties pertaining to a user's profile.
    /// <para>
    /// @author: Jennifer Nguyen, Andrew Kao
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DisplayPictureUrl { get; set; }
        public string DisplayName { get; set; }
    }
}