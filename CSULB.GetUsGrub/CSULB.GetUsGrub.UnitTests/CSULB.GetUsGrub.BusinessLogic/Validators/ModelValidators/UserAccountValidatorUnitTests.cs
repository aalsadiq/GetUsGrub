using CSULB.GetUsGrub.BusinessLogic;
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
            var userAccount = new UserAccount(username: "username", password: "!Q2w#E4r", isActive: true, isFirstTimeUser: false, roleType: "public");

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
            var userAccount = new UserAccount(username: "", password: "!Q2w#E4r", isActive: true, isFirstTimeUser: false, roleType: "public");

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
            var userAccount = new UserAccount(username: null, password: "!Q2w#E4r", isActive: true, isFirstTimeUser: false, roleType: "public");

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
            var userAccount = new UserAccount(username: "this is a fail username", password: "!Q2w#E4r", isActive: true, isFirstTimeUser: false, roleType: "public");

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
            var userAccount = new UserAccount(username: "!@#$!@!#", password: "!Q2w#E4r", isActive: true, isFirstTimeUser: false, roleType: "public");

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
            var userAccount = new UserAccount(username: "username", password: "", isActive: true, isFirstTimeUser: false, roleType: "public");

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
            var userAccount = new UserAccount(username: "username", password: null, isActive: true, isFirstTimeUser: false, roleType: "public");

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailSsoValidation_When_UsernameIsEmpty()
        {
            // Arrange
            var userAccountValidator = new UserAccountValidator();
            var userAccount = new UserAccount(username: "", password: "!Q2w#E4r", isActive: false, isFirstTimeUser: false, roleType: "public");

            // Act
            var result = userAccountValidator.Validate(userAccount, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            result.IsValid.Should().BeFalse();
            isValid.Should().Be(false);
        }
    }
}
