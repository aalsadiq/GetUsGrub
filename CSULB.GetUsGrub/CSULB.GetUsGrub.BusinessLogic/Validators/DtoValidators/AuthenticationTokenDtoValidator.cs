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
                RuleFor(authenticationTokenDto => authenticationTokenDto.TokenString)
                    .NotEmpty()
                    .NotNull();

                RuleFor(authenticationTokenDto => authenticationTokenDto.ExpiresOn)
                    .NotEmpty()
                    .NotNull();

                RuleFor(authenticationTokenDto => authenticationTokenDto.Username)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
