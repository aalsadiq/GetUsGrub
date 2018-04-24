using CSULB.GetUsGrub.Models;
using FluentValidation;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SsoTokenPayloadDtoValidator</c> class.
    /// Defines rules to validate a SsoTokenPayload.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class SsoTokenPayloadDtoValidator : AbstractValidator<SsoTokenPayloadDto>
    {
        private readonly List<string> _validRoleTypes = new List<string> { RoleTypes.PUBLIC, RoleTypes.PRIVATE };

        public SsoTokenPayloadDtoValidator()
        {
            RuleSet("SsoRegistration", () =>
            {
                RuleFor(ssoTokenPayloadDto => ssoTokenPayloadDto.Username)
                    .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                    .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);

                RuleFor(ssoTokenPayloadDto => ssoTokenPayloadDto.Password)
                    .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                    .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                    .Matches(RegularExpressions.USERNAME_FORMAT);

                RuleFor(ssoTokenPayloadDto => ssoTokenPayloadDto.RoleType)
                    .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                    .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                    .Must(roleType => _validRoleTypes.Contains(roleType));

                RuleFor(ssoTokenPayloadDto => ssoTokenPayloadDto.Application)
                    .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                    .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                    .Equal(SsoTokenPayloadValues.APPLICATION).WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);

                RuleFor(ssoTokenPayloadDto => ssoTokenPayloadDto.IssuedAt)
                    .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                    .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);
            });
        }
    }
}
