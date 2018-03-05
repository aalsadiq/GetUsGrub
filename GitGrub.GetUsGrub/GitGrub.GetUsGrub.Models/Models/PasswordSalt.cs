using FluentValidation.Attributes;

namespace GitGrub.GetUsGrub.Models
{
    [Validator(typeof(PasswordSaltValidator))]
    public class PasswordSalt : ISalt
    {
        public string Salt { get; set; }
    }
}
