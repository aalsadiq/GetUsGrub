using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>UserProfileValidatorUnitTests</c> class.
    /// Contains unit tests for UserProfileValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserProfileValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var userProfileValidator = new UserProfileValidator();
            var userProfile = new UserProfile()
            {
                DisplayName = "displayname"
            };

            // Act
            var result = userProfileValidator.Validate(userProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_DisplayNameIsEmpty()
        {
            // Arrange
            var userProfileValidator = new UserProfileValidator();
            var userProfile = new UserProfile()
            {
                DisplayName = ""
            };

            // Act
            var result = userProfileValidator.Validate(userProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_DisplayNameIsNull()
        {
            // Arrange
            var userProfileValidator = new UserProfileValidator();
            var userProfile = new UserProfile()
            {
                DisplayName = null
            };

            // Act
            var result = userProfileValidator.Validate(userProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
