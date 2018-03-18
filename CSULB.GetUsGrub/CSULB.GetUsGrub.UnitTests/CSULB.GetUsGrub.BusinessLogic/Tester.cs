using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Security.Claims;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class Tester
    {
        [Fact]
        public void Should_SetAsCollection_When_GivenClaimsJson()
        {
            // Arrange
            var claims = new Collection<Claim>()
            {
                new Claim("ReadIndividualProfile", "True"),
                new Claim("WriteIndividualProfile", "True")
            };

            // Act
            var claimsJson = JsonConvert.SerializeObject(claims);

            // Assert
            claimsJson.Should().BeOfType<string>();
            claimsJson.Should().Be("asadasdsadsad");
        }
    }
}
