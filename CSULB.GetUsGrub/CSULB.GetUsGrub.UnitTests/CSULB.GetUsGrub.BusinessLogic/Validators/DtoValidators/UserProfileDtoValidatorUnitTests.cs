using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>UserProfileDtoValidatorUnitTests</c> class.
    /// Contains unit tests for UserProfileDtoValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserProfileDtoValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var userProfileDtoValidator = new UserProfileDtoValidator();
            var userProfileDto = new UserProfileDto()
            {
                DisplayName = "displayname"
            };

            // Act
            var result = userProfileDtoValidator.Validate(userProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_DisplayNameIsEmpty()
        {
            // Arrange
            var userProfileDtoValidator = new UserProfileDtoValidator();
            var userProfileDto = new UserProfileDto()
            {
                DisplayName = ""
            };

            // Act
            var result = userProfileDtoValidator.Validate(userProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Display name is required.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_DisplayNameIsNull()
        {
            // Arrange
            var userProfileDtoValidator = new UserProfileDtoValidator();
            var userProfileDto = new UserProfileDto()
            {
                DisplayName = null
            };

            // Act
            var result = userProfileDtoValidator.Validate(userProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Display name is required.");
            errors[1].ToString().Should().Be("Display name is required.");
        }
    }
}
