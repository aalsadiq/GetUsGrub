namespace GitGrub.GetUsGrub.Models
{
    public interface IUserAccount
    {
        string Username { get; set; }
        string Password { get; set; }
        bool IsActive { get; set; }
    }
}
