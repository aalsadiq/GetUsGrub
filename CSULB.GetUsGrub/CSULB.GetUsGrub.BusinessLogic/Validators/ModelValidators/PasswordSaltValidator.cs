using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>PasswordSaltValidator</c> class.
    /// Defines rules to validate a PasswordSalt.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class PasswordSaltValidator : AbstractValidator<PasswordSalt>
    {
        public PasswordSaltValidator()
        {
            RuleFor(x => x.Salt)
                .NotEmpty()
                .NotNull();
        }
    }
}