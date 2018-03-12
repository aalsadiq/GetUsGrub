using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>RestaurantProfileMapperUnitTests</c> class.
    /// Contains unit tests for RestaurantProfileMapper.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileMapperUnitTests
    {
        [Fact]
        public void Should_MapModelToDto_When_AllFieldsFromModelAreMappedToDto()
        {
            // Arrange
            var business1 = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = "23:00"
            };
            var business2 = new BusinessHour()
            {
                Day = "Tuesday",
                OpenTime = "8:00",
                CloseTime = "23:00"
            };
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };
            var restaurantProfile = new RestaurantProfile()
            {
                Id = 0,
                UserId = 0,
                BusinessHours = new List<BusinessHour>
                {
                    business1,
                    business2
                },
                PhoneNumber = "(562)985-4111",
                Address = address,
                Longitude = 33.7838,
                Latitude = -118.1141
            };
            var restaurantProfileMapper = new RestaurantProfileMapper();

            // Act
            var restaurantProfileDto = restaurantProfileMapper.MapModelToDto(restaurantProfile);

            // Assert
            restaurantProfileDto.BusinessHours[0].Should().Be(business1);
            restaurantProfileDto.BusinessHours[1].Should().Be(business2);
            restaurantProfileDto.PhoneNumber.Should().Be("(562)985-4111");
            restaurantProfileDto.Longitude.Should().Be(33.7838);
            restaurantProfileDto.Latitude.Should().Be(-118.1141);
        }

        [Fact]
        public void Should_MapDtoToModel_When_AllFieldsFromDtoAreMappedToModel()
        {
            // Arrange
            var business1 = new BusinessHour()
            {
                Day = "Monday",
                OpenTime = "8:00",
                CloseTime = "23:00"
            };
            var business2 = new BusinessHour()
            {
                Day = "Tuesday",
                OpenTime = "8:00",
                CloseTime = "23:00"
            };
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };
            var restaurantProfileDto = new RestaurantProfileDto()
            {
                BusinessHours = new List<BusinessHour>
                {
                    business1,
                    business2
                },
                PhoneNumber = "(562)985-4111",
                Address = address,
                Longitude = 33.7838,
                Latitude = -118.1141
            };
            var restaurantProfileMapper = new RestaurantProfileMapper();

            // Act
            var restaurantProfile = restaurantProfileMapper.MapDtoToModel(restaurantProfileDto);

            // Assert
            restaurantProfile.Id.Should().Be(null);
            restaurantProfile.UserId.Should().Be(null);
            restaurantProfile.BusinessHours[0].Should().Be(business1);
            restaurantProfile.BusinessHours[1].Should().Be(business2);
            restaurantProfile.PhoneNumber.Should().Be("(562)985-4111");
            restaurantProfile.Longitude.Should().Be(33.7838);
            restaurantProfile.Latitude.Should().Be(-118.1141);
        }
    }
}
