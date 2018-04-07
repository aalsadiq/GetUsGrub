using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class AuthenticationTokenDtoValidator : AbstractValidator<AuthenticationTokenDto>
    {

        public AuthenticationTokenDtoValidator()
        {
            RuleSet("TokenRules", () =>
            {
                RuleFor(AuthenticationToken => AuthenticationToken.TokenString)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.ExpiresOn)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.Username)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
