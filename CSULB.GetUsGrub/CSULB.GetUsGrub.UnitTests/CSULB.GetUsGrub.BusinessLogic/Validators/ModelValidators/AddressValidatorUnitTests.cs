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
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };

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
            var address = new Address()
            {
                Street1 = "",
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };

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
            var address = new Address()
            {
                Street1 = null,
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };

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
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "",
                State = "CA",
                Zip = 90840
            };

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Address needs a city.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_CityIsNull()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = null,
                State = "CA",
                Zip = 90840
            };

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Address needs a city.");
            errors[1].ToString().Should().Be("Address needs a city.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_StateIsEmpty()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "",
                Zip = 90840
            };

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Address needs a state.");
            errors[1].ToString().Should().Be("State must be CA.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_StateIsNull()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = null,
                Zip = 90840
            };

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Address needs a state.");
            errors[1].ToString().Should().Be("Address needs a state.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_StateIsNotCa()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "NY",
                Zip = 90840
            };

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("State must be CA.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_ZipIsEmpty()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "CA",
            };

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Address needs a zip code.");
            errors[1].ToString().Should().Be("Zip code must contain 5 numbers.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_ZipIsGreaterThan5DigitsLong()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "CA",
                Zip = 100000
            };

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Zip code must contain 5 numbers.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_ZipIsLessThan5DigitsLong()
        {
            // Arrange
            var addressValidator = new AddressValidator();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "CA",
                Zip = 0001
            };

            // Act
            var result = addressValidator.Validate(address, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Zip code must contain 5 numbers.");
        }
    }
}
