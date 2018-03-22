using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class GoogleTimezoneIntegrationTest
    {
        [Fact]
        public async void Should_Pass_When_Getting_Csulb_Timezone()
        {
            var service = new GoogleTimezoneService();
            var coordinates = new GeoCoordinates
            {
                Latitude = 33.7830608f,
                Longitude = -118.1148909f
            };

            var result = await service.GetOffsetAsync(coordinates);

            result.Should().Be(0);
        }

        //[Fact]
        public async void Should_Fail_When_Geocoding_Csulb_Wrong_Address()
        {
            var service = new GoogleGeocodeService();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };
            var expectedLat = 0;
            var expectedLong = 0;

            var result = await service.GeocodeAsync(address);

            result.Latitude.Should().NotBe(expectedLat);
            result.Longitude.Should().NotBe(expectedLong);
        }

        //[Fact]
        public async void Should_Fail_When_Geocoding_Nonexistant_Address()
        {
            var service = new GoogleGeocodeService();
            var address = new Address()
            {
                Street1 = "9999 Bvasdawe",
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };

            var result = await service.GeocodeAsync(address);

            result.Should().BeNull();
        }
    }
}