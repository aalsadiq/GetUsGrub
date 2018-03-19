using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class AuthenticationTokenValidator : AbstractValidator<AuthenticationToken>
    {
        public AuthenticationTokenValidator()
        {
            RuleSet("TokenWithSalt", () =>
                {
                    RuleFor(x => x.TokenString)
                        .NotEmpty()
                        .NotNull();

                    RuleFor(x => x.ExpiresOn)
                        .NotEmpty()
                        .NotNull();

                    RuleFor(x => x.Username)
                        .NotEmpty()
                        .NotNull();

                    RuleFor(x => x.Salt)
                        .NotEmpty()
                        .NotNull();
                }
                );
            RuleSet("TokenNoSalt", () =>
            {
                RuleFor(x => x.TokenString)
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
