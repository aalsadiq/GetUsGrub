namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface ISalt
    {
        int Id { get; set; }
        int UserId { get; set; }
        string Salt { get; set; }
    }
}
