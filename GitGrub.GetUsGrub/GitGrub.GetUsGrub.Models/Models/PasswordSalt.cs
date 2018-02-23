namespace GitGrub.GetUsGrub.Models
{
    public class PasswordSalt : ISalt
    {
        public int Id { get; set; }

        public string Salt { get; set; }
    }
}
