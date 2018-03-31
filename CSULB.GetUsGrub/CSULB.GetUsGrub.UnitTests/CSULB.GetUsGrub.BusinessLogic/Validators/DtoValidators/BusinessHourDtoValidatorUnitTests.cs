using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>BusinessHourDtoValidatorUnitTests</c> class.
    /// Contains unit tests for BusinessHourDtoValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class BusinessHourDtoValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_DayIsEmpty()
        {
            // Arrange
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "",
                OpenTime = "8:00",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
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
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = null,
                OpenTime = "8:00",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
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
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "Monday",
                OpenTime = "",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
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
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "Monday",
                OpenTime = null,
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
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
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "Monday",
                OpenTime = "8",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
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
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = ""
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
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
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = null
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
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
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = "23"
            };

            // Act
            var result = businessHourDtoValidator.Validate(businessHourDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Time must be from 0:00 to 23:59.");
        }

        [Fact]
        public void Should_FailValidtion_When_OpenTimeIsAfterCloseTime()
        {
            // Arrange
            var businessHourDtoValidator = new BusinessHourDtoValidator();
            var businessHourDto = new BusinessHourDto()
            {
                Day = "Monday",
                OpenTime = "23:12",
                CloseTime = "23:00"
            };

            // Act
            var result = businessHourDtoValidator.CheckIfOpenTimeIsBeforeCloseTime(businessHourDto.OpenTime, businessHourDto.CloseTime);

            // Assert
            result.Should().Be(false);
        }
    }
}
