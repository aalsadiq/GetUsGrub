using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class RestaurantBillSplitterManagerUnitTests
    {
        // Arrange
        public RestaurantBillSplitterManager Manager = new RestaurantBillSplitterManager();

        [Fact]
        public void Should_GetRestaurantMenus_When_GivenValidRestaurantId()
        {
            // Arrange
            int restaurantId = 32;

            // Act
            var result = Manager.GetRestaurantMenus(restaurantId);

            // Assert
            result.Data.Should().BeOfType<RestaurantMenusDto>();
            result.Error.Should().BeNull();
        }

        [Fact]
        public void Should_NotGetRestaurantMenus_When_GivenInvalidRestaurantId()
        {
            // Arrange
            int restaurantId = 2;

            // Act
            var result = Manager.GetRestaurantMenus(restaurantId);

            // Assert
            result.Error.Should().NotBeNullOrEmpty();
            result.Error.Should().Be(GeneralErrorMessages.GENERAL_ERROR);
        }
    }
}
