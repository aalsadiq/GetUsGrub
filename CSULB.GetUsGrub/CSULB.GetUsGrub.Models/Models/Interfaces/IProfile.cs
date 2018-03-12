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
        string ProfileName { get; set; }
        string ProfilePicture { get; set; }
    }
}