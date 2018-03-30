using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>RestaurantProfileValidatorUnitTests</c> class.
    /// Contains unit tests for RestaurantProfileValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_AllRulesPass()
        {
            // Arrange
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var restaurantProfile = new RestaurantProfile()
            {
                PhoneNumber = "(562)985-4111",
                Address = new Address()
                {
                    Street1 = "1250 Bellflower Blvd",
                    City = "Long Beach",
                    State = "CA",
                    Zip = 90840
                },
                Longitude = 33.7838,
                Latitude = -118.1141
            };

            // Act
            var result = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailValidation_When_PhoneNumberIsEmpty()
        {
            // Arrange
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var restaurantProfile = new RestaurantProfile()
            {
                PhoneNumber = "",
                Address = new Address()
                {
                    Street1 = "1250 Bellflower Blvd",
                    City = "Long Beach",
                    State = "CA",
                    Zip = 90840
                },
                Longitude = 33.7838,
                Latitude = -118.1141
            };

            // Act
            var result = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_PhoneNumberIsNull()
        {
            // Arrange
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var restaurantProfile = new RestaurantProfile()
            {
                PhoneNumber = null,
                Address = new Address()
                {
                    Street1 = "1250 Bellflower Blvd",
                    City = "Long Beach",
                    State = "CA",
                    Zip = 90840
                },
                Longitude = 33.7838,
                Latitude = -118.1141
            };

            // Act
            var result = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_PhoneNumberIsNotCorrectFormat()
        {
            // Arrange
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var restaurantProfile = new RestaurantProfile()
            {
                PhoneNumber = "562-985-4111",
                Address = new Address()
                {
                    Street1 = "1250 Bellflower Blvd",
                    City = "Long Beach",
                    State = "CA",
                    Zip = 90840
                },
                Longitude = 33.7838,
                Latitude = -118.1141
            };

            // Act
            var result = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_AddressIsEmpty()
        {
            // Arrange
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var restaurantProfile = new RestaurantProfile()
            {
                PhoneNumber = "(562)985-4111",
                Longitude = 33.7838,
                Latitude = -118.1141
            };

            // Act
            var result = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_AddressIsNull()
        {
            // Arrange
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var restaurantProfile = new RestaurantProfile()
            {
                PhoneNumber = "(562)985-4111",
                Address = null,
                Longitude = 33.7838,
                Latitude = -118.1141
            };

            // Act
            var result = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_LongitudeIsEmpty()
        {
            // Arrange
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var restaurantProfile = new RestaurantProfile()
            {
                PhoneNumber = "(562)985-4111",
                Address = null,
                Latitude = -118.1141
            };

            // Act
            var result = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailValidation_When_LatitudeIsEmpty()
        {
            // Arrange
            var restaurantProfileValidator = new RestaurantProfileValidator();
            var restaurantProfile = new RestaurantProfile()
            {
                PhoneNumber = "(562)985-4111",
                Address = null,
                Longitude = 33.7838,
            };

            // Act
            var result = restaurantProfileValidator.Validate(restaurantProfile, ruleSet: "CreateUser");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
