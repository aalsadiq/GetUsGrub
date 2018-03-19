using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>BusinessHourValidatorUnitTests</c> class.
    /// Contains unit tests for BusinessHourValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class BusinessHourValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = "23:00",
                TimeZone = "PST"
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_DayIsEmpty()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = "",
                OpenTime = "8:00",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Business day is required.");
        }

        [Fact]
        public void Should_FailValidation_When_DayIsNull()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = null,
                OpenTime = "8:00",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Business day is required.");
            errors[1].ToString().Should().Be("Business day is required.");
        }

        [Fact]
        public void Should_FailValidation_When_OpenTimeIsEmpty()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = "",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Open time is required.");
            errors[1].ToString().Should().Be("Time must be from 0:00 to 23:59.");
        }

        [Fact]
        public void Should_FailValidation_When_OpenTimeIsNull()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = null,
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Open time is required.");
            errors[1].ToString().Should().Be("Open time is required.");
        }

        [Fact]
        public void Should_FailValidation_When_OpenTimeIsNotIn24HourFormat()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = "8",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Time must be from 0:00 to 23:59.");
        }

        [Fact]
        public void Should_FailValidation_When_CloseTimeIsEmpty()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = ""
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Close time is required.");
            errors[1].ToString().Should().Be("Time must be from 0:00 to 23:59.");
        }

        [Fact]
        public void Should_FailValidation_When_CloseTimeIsNull()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = null
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Close time is required.");
            errors[1].ToString().Should().Be("Close time is required.");
        }

        [Fact]
        public void Should_FailValidation_When_CloseTimeIsNotIn24HourFormat()
        {
            // Arrange
            var businessHourValidator = new BusinessHourValidator();
            var businessHour = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = "23"
            };

            // Act
            var result = businessHourValidator.Validate(businessHour, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Time must be from 0:00 to 23:59.");
        }
    }
}
