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
                RuleFor(securityQuestionDto => securityQuestionDto.Question)
                    .NotEmpty().WithMessage(ValidationErrorMessages.SECURITY_QUESTION_REQUIRED)
                    // Validation will fail if Question is less than 0
                    .GreaterThan(0).WithMessage(GeneralErrorMessages.GENERAL_ERROR);

                RuleFor(securityQuestionDto => securityQuestionDto.Answer)
                    .NotEmpty().WithMessage(ValidationErrorMessages.SECURITY_QUESTION_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.SECURITY_QUESTION_REQUIRED);
            });
        }
    }
}
