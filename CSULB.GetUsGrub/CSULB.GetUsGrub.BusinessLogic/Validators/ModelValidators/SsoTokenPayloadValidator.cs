using CSULB.GetUsGrub.Models;
using FluentValidation;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SsoTokenPayloadValidator</c> class.
    /// Defines rules to validate a SsoTokenPayload.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class SsoTokenPayloadValidator : AbstractValidator<SsoTokenPayload>
    {
        private readonly List<string> _validRoleTypes = new List<string> { RoleTypes.PUBLIC, RoleTypes.PRIVATE };

        public SsoTokenPayloadValidator()
        {
            RuleFor(ssoTokenPayload => ssoTokenPayload.Username)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);

            RuleFor(ssoTokenPayload => ssoTokenPayload.Password)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);

            RuleFor(ssoTokenPayload => ssoTokenPayload.RoleType)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .Must(roleType => _validRoleTypes.Contains(roleType));

            RuleFor(ssoTokenPayload => ssoTokenPayload.Application)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .Equal(SsoTokenPayloadValues.APPLICATION).WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);

            RuleFor(ssoTokenPayload => ssoTokenPayload.IssuedAt)
                .NotEmpty().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD)
                .NotNull().WithMessage(SsoErrorMessages.INVALID_TOKEN_PAYLOAD);
        }
        
        // TODO: @Jenn Add validation to check that the size of the list of keyvaluepairs are equal to the size of the enum. Possibly just add it to the TokenValidator [-Jenn]
    }
}
