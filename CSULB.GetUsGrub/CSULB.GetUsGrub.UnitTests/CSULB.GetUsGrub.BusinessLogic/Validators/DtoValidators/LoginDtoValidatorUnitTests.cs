using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class LoginDtoValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "username", password: "password");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_UsernameIsEmpty()
        {
            // Arrange
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "", password: "password");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
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
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: null, password: "password");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
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
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "this is a fail username", password: "password");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
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
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "!@#$%^&*()", password: "password");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
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
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "username", password: "");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
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
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "username", password: null);

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
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
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "username", password: "hi!");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
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
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "username", password: "resultshouldbefalseisvalidgiventhispasswordismorethan64characterslong");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
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
            var loginDtoValidator = new LoginDtoValidator();
            var loginDto = new LoginDto(username: "username", password: "this is a fail password");

            // Act
            var result = loginDtoValidator.Validate(loginDto, ruleSet: "UsernameAndPassword");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Password must not be empty or contain spaces.");
        }


    }
}
