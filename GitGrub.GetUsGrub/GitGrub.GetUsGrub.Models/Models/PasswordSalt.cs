using GitGrub.GetUsGrub.Models.Interfaces;

namespace GitGrub.GetUsGrub.Models.Models
{
    public class PasswordSalt : ISalt
    {
        public int Id { get; set; }
        public string Salt { get; set; }
    }
}
