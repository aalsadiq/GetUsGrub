using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class GoogleGeocodeIntegrationTest
    {
        [Fact]
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
            
            result?.Data.Latitude.Should().Equals(expectedLat);
            result?.Data.Longitude.Should().Equals(expectedLong);
        }

        [Fact]
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

            result?.Data.Latitude.Should().NotBe(expectedLat);
            result?.Data.Longitude.Should().NotBe(expectedLong);
        }

        [Fact]
        public async void Should_Fail_When_Geocoding_Nonexistant_Address()
        {
            var expected = "Invalid Input";
            var service = new GoogleGeocodeService();
            var address = new Address()
            {
                Street1 = "9999 Bvasdawe",
                City = "Long Beach",
                State = "CA",
                Zip = 90840
            };

            var result = await service.GeocodeAsync(address);

            result.Error.Should().Be(expected);
        }

        [Fact]
        public void Should_Pass_When_Geocoding_Csulb_Synchronously()
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

            var result = service.Geocode(address);

            result?.Data.Latitude.Should().Equals(expectedLat);
            result?.Data.Longitude.Should().Equals(expectedLong);
        }
    }
}