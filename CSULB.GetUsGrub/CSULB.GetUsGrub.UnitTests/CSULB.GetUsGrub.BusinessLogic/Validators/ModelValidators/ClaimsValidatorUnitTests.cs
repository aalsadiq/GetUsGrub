using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.UserAccessControl;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>ClaimsValidatorUnitTests</c> class.
    /// Contains unit tests for ClaimsValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/13/2018
    /// </para>
    /// </summary>
    public class ClaimsValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var claimsValidator = new ClaimsValidator();
            var claimsFactory = new ClaimsFactory();
            var claims = new UserClaims()
            {
                Claims = claimsFactory.Create(AccountType.Individual)
            };

            // Act
            var result = claimsValidator.Validate(claims);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_ClaimsIsNull()
        {
            // Arrange
            var claimsValidator = new ClaimsValidator();
            var claims = new UserClaims()
            {
                Claims = null
            };


            // Act
            var result = claimsValidator.Validate(claims);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_ClaimsIsEmpty()
        {
            // Arrange
            var claimsValidator = new ClaimsValidator();
            var claims = new UserClaims();

            // Act
            var result = claimsValidator.Validate(claims);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
