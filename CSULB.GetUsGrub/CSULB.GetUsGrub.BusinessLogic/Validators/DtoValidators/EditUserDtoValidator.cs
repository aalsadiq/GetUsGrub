using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class EditUserDtoValidator: AbstractValidator<EditUserDto>
    {
        public EditUserDtoValidator()
        {
            RuleSet("Username", () =>
            {
                RuleFor(userAccount => userAccount.Username)
                    .NotEmpty().WithMessage("Username is required.")
                    .NotNull().WithMessage("Username is required.")
                    .Matches(@"^[A-Za-z\d]+$").WithMessage("Username must not contain spaces and special characters.");
            });

            RuleSet("NewUserName", () =>
            {
                RuleFor(userAccount => userAccount.NewUsername)
                    .Matches(@"^[A-Za-z\d]+$").WithMessage("Username must not contain spaces and special characters.");
            });
        }
    }
}
