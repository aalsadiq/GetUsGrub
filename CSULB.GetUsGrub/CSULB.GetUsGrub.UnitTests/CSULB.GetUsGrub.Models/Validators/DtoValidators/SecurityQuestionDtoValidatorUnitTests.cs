using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>SecurityQuestiontDtoValidatorUnitTests</c> class.
    /// Contains unit tests for SecurityQuestionDtoValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionDtoValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var securityQuestionDtoValidator = new SecurityQuestionDtoValidator();
            var securityQuestionDto = new SecurityQuestionDto()
            {
                Question = 1,
                Answer = "answer"
            };

            // Act
            var result = securityQuestionDtoValidator.Validate(securityQuestionDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_QuestionIsEmpty()
        {
            // Arrange
            var securityQuestionDtoValidator = new SecurityQuestionDtoValidator();
            var securityQuestionDto = new SecurityQuestionDto()
            {
                Answer = "answer"
            };

            // Act
            var result = securityQuestionDtoValidator.Validate(securityQuestionDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Must answer 3 security questions.");
            errors[1].ToString().Should().Be("Something went wrong. Please try again later.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_QuestionIsLessThanOrEqualToZero()
        {
            // Arrange
            var securityQuestionDtoValidator = new SecurityQuestionDtoValidator();
            var securityQuestionDto = new SecurityQuestionDto()
            {
                Question = 0,
                Answer = "answer"
            };

            // Act
            var result = securityQuestionDtoValidator.Validate(securityQuestionDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Must answer 3 security questions.");
            errors[1].ToString().Should().Be("Something went wrong. Please try again later.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_AnswerIsEmpty()
        {
            // Arrange
            var securityQuestionDtoValidator = new SecurityQuestionDtoValidator();
            var securityQuestionDto = new SecurityQuestionDto()
            {
                Question = 1,
                Answer = ""
            };

            // Act
            var result = securityQuestionDtoValidator.Validate(securityQuestionDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Must answer 3 security questions.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_AnswerIsNull()
        {
            // Arrange
            var securityQuestionDtoValidator = new SecurityQuestionDtoValidator();
            var securityQuestionDto = new SecurityQuestionDto()
            {
                Question = 1,
                Answer = null
            };

            // Act
            var result = securityQuestionDtoValidator.Validate(securityQuestionDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Must answer 3 security questions.");
            errors[1].ToString().Should().Be("Must answer 3 security questions.");
        }
    }
}
