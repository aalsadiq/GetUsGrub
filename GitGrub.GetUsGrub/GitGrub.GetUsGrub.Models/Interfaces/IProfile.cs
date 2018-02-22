namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Interface representing basic profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    interface IProfile
    {
        int Id { get; set; }
        string ProfileName { get; set; }
        string ProfilePicture { get; set; }
    }
}