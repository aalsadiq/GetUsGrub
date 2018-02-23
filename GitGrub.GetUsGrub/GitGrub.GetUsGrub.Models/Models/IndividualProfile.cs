namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Regular user profile class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public class IndividualProfile : IProfile
    {
        public int Id { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }
    }
}