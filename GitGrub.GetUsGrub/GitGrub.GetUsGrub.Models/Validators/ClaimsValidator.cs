using FluentValidation;

namespace GitGrub.GetUsGrub.Models.Validators
{
    public class ClaimsValidator : AbstractValidator<Claims>
    {
        /// <summary>
        /// The <c>ClaimsValidator</c> class.
        /// Defines rules for validating Claims.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/06/2018
        /// </para>
        /// </summary>
        public ClaimsValidator()
        {
            RuleFor(x => x.ClaimsList)
                .NotEmpty()
                .NotNull();
        }
    }
}
