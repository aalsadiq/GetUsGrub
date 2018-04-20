using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class SsoTokenPayloadDtoValidatorUnitTests
    {
        private readonly SsoTokenPayloadDtoValidator _ssoTokenPayloadDtoValidator = new SsoTokenPayloadDtoValidator();

        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "getusgrub",
                RoleType = "public",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_UsernameIsEmpty()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "",
                Password = "password01",
                Application = "getusgrub",
                RoleType = "public",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_UsernameIsNull()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = null,
                Password = "password01",
                Application = "getusgrub",
                RoleType = "public",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_PasswordIsEmpty()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "",
                Application = "getusgrub",
                RoleType = "public",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_PasswordIsNull()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = null,
                Application = "getusgrub",
                RoleType = "public",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_ApplicationIsEmpty()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "",
                RoleType = "public",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_ApplicationIsNull()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = null,
                RoleType = "public",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_ApplicationIsNotForOurApplication()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "anotherapp",
                RoleType = "public",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_RoleTypeIsEmpty()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "getusgrub",
                RoleType = "",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_RoleTypeIsNull()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "getusgrub",
                RoleType = null,
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_RoleTypeIsNotPublicOrPrivate()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "getusgrub",
                RoleType = "someOtherRoleType",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_RoleTypeIsPublicAndAllCapitalized()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "getusgrub",
                RoleType = "PUBLIC",
                IssuedAt = "123980213"
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_IssuedAtIsEmpty()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "getusgrub",
                RoleType = "public",
                IssuedAt = ""
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_IssuedAtIsNull()
        {
            // Arrange
            var ssoTokenPayload = new SsoTokenPayloadDto()
            {
                Username = "username01",
                Password = "password01",
                Application = "getusgrub",
                RoleType = "public",
                IssuedAt = null
            };

            // Act
            var result = _ssoTokenPayloadDtoValidator.Validate(ssoTokenPayload, ruleSet: "SsoRegistration");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
