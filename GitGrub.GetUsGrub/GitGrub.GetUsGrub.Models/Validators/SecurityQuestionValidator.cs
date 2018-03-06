using FluentValidation;

namespace GitGrub.GetUsGrub.Models.Validators
{
    /// <summary>
    /// The <c>UserAccountValidator</c> class.
    /// Defines rules to validate a UserAccount.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class SecurityQuestionValidator : AbstractValidator<ISecurityQuestion>
    {
        public SecurityQuestionValidator()
        {
            RuleSet("RegistrationPreLogic", () =>
            {
                RuleFor(x => x.QuestionType)
                    .NotEmpty().WithMessage("Must answer 3 security questions.")
                    .NotNull().WithMessage("Must answer 3 security questions.");

                RuleFor(x => x.QuestionAnswer)
                    .NotEmpty().WithMessage("Must answer 3 security questions.")
                    .NotNull().WithMessage("Must answer 3 security questions.");
            });

            RuleSet("RegistrationPostLogic", () =>
            {
                RuleFor(x => x.QuestionType)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.QuestionAnswer)
                    .NotEmpty()
                    .NotNull();

            });
        }
    }
}
