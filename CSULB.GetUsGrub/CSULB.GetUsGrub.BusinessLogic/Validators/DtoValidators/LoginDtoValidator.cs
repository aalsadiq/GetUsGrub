using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserAccountValidator</c> class.
    /// Defines rules to validate a UserAccountDto.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        // TODO @Ahmed put RegX and Error Mesages in the Constant in the Model Layer @Ahmed 
        public LoginDtoValidator()
        {
            RuleSet("UsernameAndPassword", () =>
            {
                RuleFor(loginDto => loginDto.Username)
                    .NotEmpty().WithMessage("Username is required.")
                    .NotNull().WithMessage("Username is required.")
                    .Matches(@"^[A-Za-z\d]+$").WithMessage("Username must not contain spaces and special characters.");

                RuleFor(loginDto => loginDto.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .NotNull().WithMessage("Password is required.")
                    .Length(8, 64).WithMessage("Password must be at least 8 characters and less than or equal to 64.")
                    .Matches(@"^[^\s]+$").WithMessage("Password must not be empty or contain spaces.");
            });
        }
    }
}
