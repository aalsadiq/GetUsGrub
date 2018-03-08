using FluentValidation.Attributes;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>PasswordSaltDto</c> class.
    /// Data transfer object for the PasswordSalt domain model.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/07/2018
    /// </para>
    /// </summary>
    [Validator(typeof(PasswordSaltValidator))]
    public class PasswordSaltDto : ISalt
    {
        public string Salt { get; set; }
    }
}
