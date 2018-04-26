using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit tests to validate the Food Preferences DTO
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/26/18
    /// </summary>
    public class FoodPreferencesDtoValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var validator = new FoodPreferencesDtoValidator();
            var preferences = new List<string>()
            {
                "Pescetarian",
                "Lacto-Vegetarian"
            };
            var foodPreferencesDto = new FoodPreferencesDto(preferences);

            // Act
            var result = validator.Validate(foodPreferencesDto);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Should_FailValidation_When_FoodPreferencesIsNull()
        {
            // Arrange
            var validator = new FoodPreferencesDtoValidator();
            var foodPreferencesDto = new FoodPreferencesDto();

            // Act
            var result = validator.Validate(foodPreferencesDto);

            // Assert
            result.Errors.Should().NotBeNull();
        }
    }
}
