using Xunit;
using FluentAssertions;
using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit test to validate the Food Preference model
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/14/18
    /// </summary>
    public class FoodPreferenceValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var validator = new FoodPreferenceValidator();
            var preference = new FoodPreference ("Vegan");

            // Act
            var result = validator.Validate(preference);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_FoodPreferenceIsEmpty()
        {
            // Arrange
            var validator = new FoodPreferenceValidator();
            var preference = new FoodPreference("");

            // Act
            var result = validator.Validate(preference);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_FoodPreferenceIsNull()
        {
            // Arrange
            var validator = new FoodPreferenceValidator();
            var preference = new FoodPreference();

            // Act
            var result = validator.Validate(preference);
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
