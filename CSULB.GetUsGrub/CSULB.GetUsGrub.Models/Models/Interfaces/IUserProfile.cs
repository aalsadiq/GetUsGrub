namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Basic profile interface
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>
    public interface IUserProfile
    {
        string DisplayName { get; set; }

        string DisplayPicture { get; set; }
    }
}
