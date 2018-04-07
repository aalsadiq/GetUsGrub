using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SecurityQuestionDtoValidator</c> class.
    /// Defines rules to validate a SecurityQuestionDto.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionDtoValidator : AbstractValidator<SecurityQuestionDto>
    {
        public SecurityQuestionDtoValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(User => User.Question)
                    .NotEmpty().WithMessage("Must answer 3 security questions.")
                    .GreaterThan(0).WithMessage("Something went wrong. Please try again later.");

                RuleFor(User => User.Answer)
                    .NotEmpty().WithMessage("Must answer 3 security questions.")
                    .NotNull().WithMessage("Must answer 3 security questions.");
            });
        }
    }
}
