using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class EditUserDtoValidator: AbstractValidator<EditUserDto>
    {
        public EditUserDtoValidator()
        {
            RuleSet("EditUsername", () =>
            {
                RuleFor(userAccount => userAccount.Username)
                    .NotEmpty().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .Matches(RegularExpressions.USERNAME_FORMAT).WithMessage(ValidationErrorMessages.USERNAME_FORMAT);
            });
            RuleSet("EditNewDisplayName", () =>
            {
                RuleFor(userAccount => userAccount.NewUsername)
                    .Matches(RegularExpressions.USERNAME_FORMAT).WithMessage(ValidationErrorMessages.NEWUSERNAME_FORMAT);
            });
        }
    }
}
