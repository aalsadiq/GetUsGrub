using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>SecurityAnswerSaltValidatorUnitTests</c> class.
    /// Contains unit tests for SecurityAnswerSaltValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class SecurityAnswerSaltValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var securityAnswerSaltValidator = new SecurityAnswerSaltValidator();
            var securityAnswerSalt = new SecurityAnswerSalt()
            {
                Salt = "!Q2w#E4r"
            };

            // Act
            var result = securityAnswerSaltValidator.Validate(securityAnswerSalt);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_SaltIsEmpty()
        {
            // Arrange
            var securityAnswerSaltValidator = new SecurityAnswerSaltValidator();
            var securityAnswerSalt = new SecurityAnswerSalt()
            {
                Salt = ""
            };


            // Act
            var result = securityAnswerSaltValidator.Validate(securityAnswerSalt);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_SaltIsNull()
        {
            // Arrange
            var securityAnswerSaltValidator = new SecurityAnswerSaltValidator();
            var securityAnswerSalt = new SecurityAnswerSalt()
            {
                Salt = null
            };


            // Act
            var result = securityAnswerSaltValidator.Validate(securityAnswerSalt);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
