using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>UserAccountDtoValidatorUnitTests</c> class.
    /// Contains unit tests for UserAccountDtoValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserAccountDtoValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "username",
                Password = "password"
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_UsernameIsEmpty()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "",
                Password = "password"
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Username is required.");
            errors[1].ToString().Should().Be("Username must not contain spaces and special characters.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_UsernameIsNull()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = null,
                Password = "password"
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Username is required.");
            errors[1].ToString().Should().Be("Username is required.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_UsernameContainsSpaces()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "this is a fail username",
                Password = "password"
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Username must not contain spaces and special characters.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_UsernameContainsSpecialCharacters()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "!@#$%^&*()",
                Password = "password"
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Username must not contain spaces and special characters.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_PasswordIsEmpty()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "username",
                Password = ""
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(3);
            errors[0].ToString().Should().Be("Password is required.");
            errors[1].ToString().Should().Be("Password must be at least 8 characters and less than or equal to 64.");
            errors[2].ToString().Should().Be("Password must not be empty or contain spaces.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_PasswordIsNull()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "username",
                Password = null
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Password is required.");
            errors[1].ToString().Should().Be("Password is required.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_PasswordIsLessThan8Characters()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "username",
                Password = "hi!"
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Password must be at least 8 characters and less than or equal to 64.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_PasswordIsMoreThan64Characters()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "username",
                Password = "resultshouldbefalseisvalidgiventhispasswordismorethan64characterslong"
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Password must be at least 8 characters and less than or equal to 64.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_PasswordContainsSpaces()
        {
            // Arrange
            var userAccountDtoValidator = new UserAccountDtoValidator();
            var userAccountDto = new UserAccountDto()
            {
                Username = "username",
                Password = "this is a fail password"
            };

            // Act
            var result = userAccountDtoValidator.Validate(userAccountDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Password must not be empty or contain spaces.");
        }
    }
}
