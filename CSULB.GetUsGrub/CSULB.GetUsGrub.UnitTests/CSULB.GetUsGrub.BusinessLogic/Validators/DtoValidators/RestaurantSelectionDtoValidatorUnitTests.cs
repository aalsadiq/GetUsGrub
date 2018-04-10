using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using FluentValidation;
using System;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>RestaurantSelectionDtoValidatorUnitTests</c> class.
    /// Contains unit tests for RestaurantSelectionDtoValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/04/2018
    /// </para>
    /// </summary>
    public class RestaurantSelectionDtoValidatorUnitTests
    {
        // Start of validation for PreLogic RuleSet
        [Fact]
        public void Should_PassPreLogicValidation_When_AllRulesPass()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "PreLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailPreLogicValidation_When_CityIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "",
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "PreLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailPreLogicValidation_When_CityIsNull()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = null,
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "PreLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailPreLogicValidation_When_FoodTypeIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "PreLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailPreLogicValidation_When_FoodTypeIsNull()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = null,
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "PreLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailPreLogicValidation_When_DistanceInMilesIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "American Food",
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "PreLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailPreLogicValidation_When_AvgFoodPriceIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "PreLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailPreLogicValidation_When_CurrentUtcDateTimeIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                AvgFoodPrice = 1
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "PreLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        // Start of validation for Unregistered User Post Logic
        [Fact]
        public void Should_PassUnregisteredUserPostLogicValidation_When_AllRulesPass()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "UnregisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailUnregisteredUserPostLogicValidation_When_CityIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "",
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "UnregisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailUnregisteredUserPostLogicValidation_When_CityIsNull()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = null,
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "UnregisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailUnregisteredUserPostLogicValidation_When_FoodTypeIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "UnregisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailUnregisteredUserPostLogicValidation_When_FoodTypeIsNull()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = null,
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "UnregisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailUnregisteredUserPostLogicValidation_When_DistanceInMilesIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "American Food",
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "UnregisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailUnregisteredUserPostLogicValidation_When_AvgFoodPriceIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "UnregisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailUnregisteredUserPostLogicValidation_When_CurrentUtcDateTimeIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                AvgFoodPrice = 1
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "UnregisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        // Start of validation for Register User PreLogic RuleSet
        [Fact]
        public void Should_PassRegisteredUserPostLogicValidation_When_AllRulesPass()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "RegisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Should_FailRegisteredUserPostLogicValidation_When_CityIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "",
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "RegisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailRegisteredUserPostLogicValidation_When_CityIsNull()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = null,
                State = "CA",
                FoodType = "American Food",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "RegisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailRegisteredUserPostLogicValidation_When_FoodTypeIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "RegisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailRegisteredUserPostLogicValidation_When_FoodTypeIsNull()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = null,
                DistanceInMiles = 10,
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "RegisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailRegisteredUserPostLogicValidation_When_DistanceInMilesIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "American Food",
                AvgFoodPrice = 1,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "RegisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailRegisteredUserPostLogicValidation_When_AvgFoodPriceIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                CurrentUtcDateTime = DateTime.UtcNow
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "RegisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }

        [Fact]
        public void Should_FailRegisteredUserPostLogicValidation_When_CurrentUtcDateTimeIsEmpty()
        {
            // Arrange
            var restaurantSelectionDtoValidator = new RestaurantSelectionDtoValidator();
            var restaurantSelectionDto = new RestaurantSelectionDto
            {
                City = "Los Angeles",
                State = "CA",
                FoodType = "",
                DistanceInMiles = 10,
                AvgFoodPrice = 1
            };

            // Act
            var result = restaurantSelectionDtoValidator.Validate(restaurantSelectionDto, ruleSet: "RegisteredUserPostLogic");
            var isValid = result.IsValid;

            // Assert
            isValid.Should().Be(false);
        }
    }
}
