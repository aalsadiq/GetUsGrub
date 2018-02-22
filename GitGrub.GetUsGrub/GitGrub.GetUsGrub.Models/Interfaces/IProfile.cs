namespace GitGrub.GetUsGrub.Models.Interfaces
{
    /// <summary>
    /// Interface representing basic profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    interface IProfile
    {
        string ProfileName { get; set; }
        string ProfilePicture { get; set; }
    }
}