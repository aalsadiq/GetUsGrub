using CSULB.GetUsGrub.Models;
using FluentValidation;
using System.Collections.Generic;
// TODO: @Jenn Unit test this [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class SsoTokenPayloadValidator : AbstractValidator<SsoTokenPayload>
    {
        private readonly List<string> _validRoleTypes = new List<string> { RoleTypes.PUBLIC, RoleTypes.PRIVATE };

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
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .Must(x => _validRoleTypes.Contains(x));

            RuleFor(x => x.Application)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .Equal(SsoTokenPayloadValues.APPLICATION).WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);

            RuleFor(x => x.IssuedAt)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);
        }
        
    }
}
