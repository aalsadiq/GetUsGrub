namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The IUserProfile interface.
    /// A contract with defined properties for the UserProfile class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public interface IUserProfile
    {
        string DisplayPictureUrl { get; }
        string DisplayName { get; }
    }
}
