using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>PasswordSaltValidatorUnitTests</c> class.
    /// Contains unit tests for PasswordSaltValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class PasswordSaltValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var passwordSaltValidator = new PasswordSaltValidator();
            var passwordSalt = new PasswordSalt()
            {
                Salt = "!Q2w#E4r"
            };

            // Act
            var result = passwordSaltValidator.Validate(passwordSalt);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_SaltIsEmpty()
        {
            // Arrange
            var passwordSaltValidator = new PasswordSaltValidator();
            var passwordSalt = new PasswordSalt()
            {
                Salt = ""
            };


            // Act
            var result = passwordSaltValidator.Validate(passwordSalt);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_SaltIsNull()
        {
            // Arrange
            var passwordSaltValidator = new PasswordSaltValidator();
            var passwordSalt = new PasswordSalt()
            {
                Salt = null
            };


            // Act
            var result = passwordSaltValidator.Validate(passwordSalt);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
