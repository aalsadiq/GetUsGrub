using FluentValidation.Attributes;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>PasswordSalt</c> class.
    /// Defines a property of a salt for a password.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    [Validator(typeof(PasswordSaltValidator))]
    public class PasswordSalt : ISalt
    {
        public string Salt { get; set; }
    }
}
