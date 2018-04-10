using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SecurityQuestionValidator</c> class.
    /// Defines rules to validate a SecurityQuestion.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionValidator : AbstractValidator<SecurityQuestion>
    {
        public SecurityQuestionValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(securityQuestion => securityQuestion.Question)
                    .NotEmpty()
                    .NotNull()
                    .GreaterThan(0);

                RuleFor(securityQuestion => securityQuestion.Answer)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
