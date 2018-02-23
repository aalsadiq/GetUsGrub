namespace GitGrub.GetUsGrub.Models
{
    public class TokenSalt : ISalt
    { 
        public int Id { get; set; }

        public string Salt { get; set; }
    }
}

