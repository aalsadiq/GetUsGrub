using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>RestaurantProfileDtoValidatorUnitTests</c> class.
    /// Contains unit tests for RestaurantProfileDtoValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileDtoValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
            var restaurantProfileDto = new RestaurantProfileDto()
            {
                PhoneNumber = "(562)985-4111",
                Address = new Address()
                {
                    Street1 = "1250 Bellflower Blvd",
                    City = "Long Beach",
                    State = "CA",
                    Zip = 90840
                }
            };

            // Act
            var result = restaurantProfileDtoValidator.Validate(restaurantProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_PhoneNumberIsEmpty()
        {
            // Arrange
            var restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
            var restaurantProfileDto = new RestaurantProfileDto()
            {
                PhoneNumber = "",
                Address = new Address()
                {
                    Street1 = "1250 Bellflower Blvd",
                    City = "Long Beach",
                    State = "CA",
                    Zip = 90840
                }
            };

            // Act
            var result = restaurantProfileDtoValidator.Validate(restaurantProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Phone number is required.");
            errors[1].ToString().Should().Be("Phone number must be in (XXX)XXX-XXXX format.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_PhoneNumberIsNull()
        {
            // Arrange
            var restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
            var restaurantProfileDto = new RestaurantProfileDto()
            {
                PhoneNumber = null,
                Address = new Address()
                {
                    Street1 = "1250 Bellflower Blvd",
                    City = "Long Beach",
                    State = "CA",
                    Zip = 90840
                }
            };

            // Act
            var result = restaurantProfileDtoValidator.Validate(restaurantProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Phone number is required.");
            errors[1].ToString().Should().Be("Phone number is required.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_PhoneNumberIsNotCorrectFormat()
        {
            // Arrange
            var restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
            var restaurantProfileDto = new RestaurantProfileDto()
            {
                PhoneNumber = "562-985-4111",
                Address = new Address()
                {
                    Street1 = "1250 Bellflower Blvd",
                    City = "Long Beach",
                    State = "CA",
                    Zip = 90840

                }
            };

            // Act
            var result = restaurantProfileDtoValidator.Validate(restaurantProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(1);
            errors[0].ToString().Should().Be("Phone number must be in (XXX)XXX-XXXX format.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_AddressIsEmpty()
        {
            // Arrange
            var restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
            var restaurantProfileDto = new RestaurantProfileDto()
            {
                PhoneNumber = "(562)985-4111"
            };

            // Act
            var result = restaurantProfileDtoValidator.Validate(restaurantProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Address is required.");
            errors[1].ToString().Should().Be("Address is required.");
        }

        [Fact]
        public void Should_FailValidationWithMessage_When_AddressIsNull()
        {
            // Arrange
            var restaurantProfileDtoValidator = new RestaurantProfileDtoValidator();
            var restaurantProfileDto = new RestaurantProfileDto()
            {
                PhoneNumber = "(562)985-4111",
                Address = null
            };

            // Act
            var result = restaurantProfileDtoValidator.Validate(restaurantProfileDto, ruleSet: "CreateUser");
            var isValid = result.IsValid;
            var errors = result.Errors;

            // Assert
            isValid.Should().Be(false);
            errors.Count.Should().Be(2);
            errors[0].ToString().Should().Be("Address is required.");
            errors[1].ToString().Should().Be("Address is required.");
        }
    }
}
