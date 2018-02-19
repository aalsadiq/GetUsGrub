using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Models.Models
{
    public class RegularProfile : IProfile
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }

        // Constructor
        public RegularProfile(string name, string picture)
        {
            ProfileName = name;
            ProfilePicture = picture;
        }
    }
}