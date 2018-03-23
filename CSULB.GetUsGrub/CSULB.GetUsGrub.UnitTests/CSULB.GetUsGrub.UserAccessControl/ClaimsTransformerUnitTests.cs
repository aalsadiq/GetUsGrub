using Xunit;
using System.Security.Claims;
using CSULB.GetUsGrub.UserAccessControl;
using FluentAssertions;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit testing for the ClaimsTransformer.
    /// @author: Rachel Dang
    /// @updated: 03/22/2018
    /// </summary>
    public class ClaimsTransformerUnitTests
    {
        // Arrange
        ClaimsTransformer transformer = new ClaimsTransformer();
        ClaimsFactory factory = new ClaimsFactory();

        [Fact]
        public void Should_ReturnClaimsPrincipal_With_AllClaims()
        {
            // Arrange
            var user = "username";
            var claims = new List<Claim> { new Claim("username", user) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resourceName = "permission";

            // Act
            var result = transformer.Authenticate(resourceName, principal).HasClaim("UpdatePreferences", "True");
            
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Should_ReturnClaimsPrincipalWithReadPermissions_When_UsernameIsPassed()
        {
            // Arrange
            var user = "username";
            var claims = new List<Claim> { new Claim("username", user) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resourceName = "read";

            // Act
            var allClaims = transformer.Authenticate(resourceName, principal).HasClaim("UpdatePreferences", "True");
            var readClaims = transformer.Authenticate(resourceName, principal).HasClaim("ReadPreferences", "True");
            // Assert
            allClaims.Should().BeFalse();
            readClaims.Should().BeTrue();
        }
    }
}
