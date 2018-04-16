namespace CSULB.GetUsGrub.Models
{
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