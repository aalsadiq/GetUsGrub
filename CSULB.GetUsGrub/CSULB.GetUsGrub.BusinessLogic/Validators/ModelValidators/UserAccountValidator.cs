using CSULB.GetUsGrub.Models;
using FluentValidation;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserAccountValidator</c> class.
    /// Defines rules to validate a UserAccount.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserAccountValidator : AbstractValidator<UserAccount>
    {
        private readonly List<string> _validRoleTypes = new List<string> { RoleTypes.PUBLIC, RoleTypes.PRIVATE };

        public UserAccountValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(userAccount => userAccount.Username)
                    .NotEmpty()
                    .NotNull()
                    .Matches(RegularExpressions.USERNAME_FORMAT);

                RuleFor(userAccount => userAccount.Password)
                    .NotEmpty()
                    .NotNull();

                RuleFor(userAccount => userAccount.IsActive)
                    .NotNull();

                RuleFor(userAccount => userAccount.IsFirstTimeUser)
                    .NotNull();

                RuleFor(userAccount => userAccount.RoleType)
                    .NotEmpty()
                    .NotNull()
                    .Must(roleType => _validRoleTypes.Contains(roleType));
            });

            RuleSet("SsoRegistration", () =>
            {
                RuleFor(userAccount => userAccount.Username)
                    .NotEmpty()
                    .NotNull()
                    .Matches(RegularExpressions.USERNAME_FORMAT);

                RuleFor(userAccount => userAccount.Password)
                    .NotEmpty()
                    .NotNull();

                RuleFor(userAccount => userAccount.IsFirstTimeUser)
                    .NotEmpty()
                    .NotNull();

                RuleFor(userAccount => userAccount.RoleType)
                    .NotEmpty()
                    .NotNull()
                    .Must(roleType => _validRoleTypes.Contains(roleType));
            });
        }
    }
}
