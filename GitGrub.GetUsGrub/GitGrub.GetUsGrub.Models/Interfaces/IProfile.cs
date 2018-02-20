namespace GitGrub.GetUsGrub.Models
{
    interface IProfile
    {
        int Id { get; set; }
        string ProfileName { get; set; }
        string ProfilePicture { get; set; }
    }
}