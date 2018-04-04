namespace CSULB.GetUsGrub.Models
{
    // TODO: @Andrew There is already an IUserProfile. Do you want it to be just IProfile? [-Jenn]
    /// <summary>
    /// Interface representing basic profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public interface IProfile
    {
        string DisplayName { get; set; }
        string DisplayPicture { get; set; }
    }
}