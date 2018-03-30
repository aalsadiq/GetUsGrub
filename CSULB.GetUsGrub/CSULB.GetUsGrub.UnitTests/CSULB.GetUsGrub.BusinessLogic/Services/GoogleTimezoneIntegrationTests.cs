using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class GoogleTimeZoneIntegrationTest
    {
        [Fact]
        public async void Should_Pass_When_Getting_Csulb_Timezone()
        {
            var expected = -25200;
            var service = new GoogleTimeZoneService();
            var coordinates = new GeoCoordinates
            {
                Latitude = 33.7830608,
                Longitude = -118.1148909
            };

            var result = await service.GetOffsetAsync(coordinates);

            result.Data.Should().Be(expected);
        }
    }
}