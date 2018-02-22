namespace GitGrub.GetUsGrub.Models
{
    public interface ISecurityQuestion
    {
        int Id { get; set; }

        int UserId { get; set; }

        string QuestionType { get; set; }

        string QuestionAnswer { get; set; }
    }
}
