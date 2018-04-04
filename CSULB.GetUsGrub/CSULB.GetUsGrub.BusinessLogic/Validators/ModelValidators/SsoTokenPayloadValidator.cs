using CSULB.GetUsGrub.Models;
using FluentValidation;
// TODO: @Jenn Unit test this [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class SsoTokenPayloadValidator : AbstractValidator<SsoTokenPayload>
    {
        public SsoTokenPayloadValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);

            RuleFor(x => x.RoleType)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);
            // TODO: @Jenn Invalid RoleTypes? [-Jenn]

            RuleFor(x => x.Application)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .Equal(SsoTokenPayloadValues.APPLICATION).WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);
        }
        
    }
}
