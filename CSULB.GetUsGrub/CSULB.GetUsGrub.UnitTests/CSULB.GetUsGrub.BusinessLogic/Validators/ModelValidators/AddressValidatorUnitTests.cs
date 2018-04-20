using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>AddressValidatorUnitTests</c> class.
    /// Contains unit tests for AddressValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class AddressValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address("1250 Bellflower Blvd", "Long Beach", "CA", 90840);

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_Street1IsEmpty()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address("", "Long Beach", "CA", 90840);

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Address needs a street1.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_Street1IsNull()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address(null, "Long Beach", "CA", 90840);

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Address needs a street1.");
            errors[1].ToString().Should().Be("Address needs a street1.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_CityIsEmpty()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address("1250 Bellflower Blvd", "", "CA", 90840);

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("City is required.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_CityIsNull()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address("1250 Bellflower Blvd", null, "CA", 90840);

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("City is required.");
            errors[1].ToString().Should().Be("City is required.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_StateIsEmpty()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address("1250 Bellflower Blvd", "Long Beach", "", 90840);

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("State is required." );
            errors[1].ToString().Should().Be("State be a valid state.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_StateIsNotCa()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address("1250 Bellflower Blvd", "Long Beach", "NY", 90840);

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("State be a valid state.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_ZipIsGreaterThan5DigitsLong()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address("1250 Bellflower Blvd", "Long Beach", "CA", 100000);
            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Zip code is not a valid format.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_ZipIsLessThan5DigitsLong()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address("1250 Bellflower Blvd", "Long Beach", "CA", 0001);

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Zip code is not a valid format.");
        }
    }
}
