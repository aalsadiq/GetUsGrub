using FluentValidation;

namespace GitGrub.GetUsGrub.Models.Validators
{
    public class SecurityAnswerSaltValidator : AbstractValidator<SecurityAnswerSalt>
    {
        /// <summary>
        /// The <c>SecurityAnswerSaltValidator</c> class.
        /// Defines rules for validating a SecurityAnswer.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/06/2018
        /// </para>
        /// </summary>
        public SecurityAnswerSaltValidator()
        {
            RuleFor(x => x.Salt)
                .NotEmpty()
                .NotNull();
        }
    }
}
