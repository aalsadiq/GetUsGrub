using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>UserAccountValidatorUnitTests</c> class.
    /// Contains unit tests for UserAccountValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserAccountValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var userAccountValidator = new UserAccountValidator();
            var userAccount = new UserAccount()
            {
                Username = "username",
                Password = "!Q2w#E4r",
                IsActive = true,
                FirstTimeUser = false,
            };

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_UsernameIsEmpty()
        {
            // Arrange
            var userAccountValidator = new UserAccountValidator();
            var userAccount = new UserAccount()
            {
                Username = "",
                Password = "!Q2w#E4r",
                IsActive = true,
                FirstTimeUser = false
            };

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_UsernameIsNull()
        {
            // Arrange
            var userAccountValidator = new UserAccountValidator();
            var userAccount = new UserAccount()
            {
                Username = null,
                Password = "!Q2w#E4r",
                IsActive = true,
                FirstTimeUser = false
            };

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_UsernameContainsSpaces()
        {
            // Arrange
            var userAccountValidator = new UserAccountValidator();
            var userAccount = new UserAccount()
            {
                Username = "this is a fail username",
                Password = "!Q2w#E4r",
                IsActive = true,
                FirstTimeUser = false
            };

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_UsernameContainsSpecialCharacters()
        {
            // Arrange
            var userAccountValidator = new UserAccountValidator();
            var userAccount = new UserAccount()
            {
                Username = "!@#$%^&*()",
                Password = "!Q2w#E4r",
                IsActive = true,
                FirstTimeUser = false
            };

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_PasswordIsEmpty()
        {
            // Arrange
            var userAccountValidator = new UserAccountValidator();
            var userAccount = new UserAccount()
            {
                Username = "username",
                Password = "",
                IsActive = true,
                FirstTimeUser = false
            };

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_PasswordIsNull()
        {
            // Arrange
            var userAccountValidator = new UserAccountValidator();
            var userAccount = new UserAccount()
            {
                Username = "username",
                Password = null,
                IsActive = true,
                FirstTimeUser = false
            };

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
