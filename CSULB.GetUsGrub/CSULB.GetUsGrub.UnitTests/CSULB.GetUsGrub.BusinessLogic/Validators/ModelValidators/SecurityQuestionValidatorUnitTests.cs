using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>SecurityQuestionValidatorUnitTests</c> class.
    /// Contains unit tests for SecurityQuestionValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var securityQuestionValidator = new SecurityQuestionValidator();
            var securityQuestion = new SecurityQuestion()
            {
                Question = 1,
                Answer = "answer"
            };

            // Act
            var result = securityQuestionValidator.Validate(securityQuestion, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_QuestionIsEmpty()
        {
            // Arrange
            var securityQuestionValidator = new SecurityQuestionValidator();
            var securityQuestion = new SecurityQuestion()
            {
                Answer = "answer"
            };

            // Act
            var result = securityQuestionValidator.Validate(securityQuestion, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_QuestionIsLessThanOrEqualToZero()
        {
            // Arrange
            var securityQuestionValidator = new SecurityQuestionValidator();
            var securityQuestion = new SecurityQuestion()
            {
                Question = 0,
                Answer = "answer"
            };

            // Act
            var result = securityQuestionValidator.Validate(securityQuestion, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_AnswerIsEmpty()
        {
            // Arrange
            var securityQuestionValidator = new SecurityQuestionValidator();
            var securityQuestion = new SecurityQuestion()
            {
                Question = 1,
                Answer = ""
            };

            // Act
            var result = securityQuestionValidator.Validate(securityQuestion, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_AnswerIsNull()
        {
            // Arrange
            var securityQuestionValidator = new SecurityQuestionValidator();
            var securityQuestion = new SecurityQuestion()
            {
                Question = 1,
                Answer = null
            };

            // Act
            var result = securityQuestionValidator.Validate(securityQuestion, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
