namespace GitGrub.GetUsGrub.Models
{
    public interface IUserAccount
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string AccountType { get; set; }
        bool IsActive { get; set; }
    }
}
