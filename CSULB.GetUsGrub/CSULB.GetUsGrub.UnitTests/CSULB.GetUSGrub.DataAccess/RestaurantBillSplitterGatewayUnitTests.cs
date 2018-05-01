using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class RestaurantBillSplitterGatewayUnitTests
    {
        // Arrange
        public RestaurantBillSplitterGateway Gateway = new RestaurantBillSplitterGateway();

        [Fact]
        public void Should_NotReturnErrors_When_GivenValidRestaurantExistingInDatabase()
        {
            // Arrange
            int restaurantId = 32;

            // Act
            var result = Gateway.GetRestaurantMenus(restaurantId);

            // Assert
            result.Error.Should().BeNull();
        }

        [Fact]
        public void Should_ReturnErrors_When_GivenInvalidRestaurantNotExistingInDatabase()
        {
            // Arrange
            int restaurantId = 1;

            // Act
            var result = Gateway.GetRestaurantMenus(restaurantId);

            // Assert
            result.Data.Should().BeNull();
            result.Error.Should().Be(GeneralErrorMessages.GENERAL_ERROR);
        }
    }
}
