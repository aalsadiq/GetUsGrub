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
    public class GeocodeIntegrationTest
    {
        [Fact]
        public async void Should_Pass_When_Hitting_Quota_Per_Second()
        {
            var service = new GoogleGeocodeService();
            var expected = 50;
            int actual = await NewMethod(service);

            actual.Should().Be(-1);
        }

        private static async Task<int> NewMethod(GoogleGeocodeService service)
        {
            return await service.TestQuotaLimit();
        }

        //[Fact]
        public async void Should_Pass_When_Geocoding_Csulb()
        {
            var service = new GoogleGeocodeService();
            var address = new Address()
            {
                Street1 = "1250 Bellflower Blvd",
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };
            var expectedLat = 33.7830608;
            var expectedLong = -118.1148909;

            var result = await service.GeocodeAsync(address);
            
            result.Latitude.Should().Equals(expectedLat);
            result.Longitude.Should().Equals(expectedLong);
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