using Xunit;
using System.Security.Claims;
using CSULB.GetUsGrub.UserAccessControl;
using System;
using System.Security;

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
        public void Should_ReturnClaimsPrincipalWithAllPermissions_When_UsernameIsPassed()
        {
            // Arrange
            var claims = factory.CreateAdminClaims();
            claims.Add(new Claim("username", "Admin"));
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resourceName = "permission";

            // Act
            var actual = transformer.Authenticate(resourceName, principal);
            var expected = typeof(ClaimsPrincipal);

            // Assert
            Assert.IsType(expected, actual);
        }

        [Fact]
        public void Should_ReturnClaimsPrincipalWithReadPermissions_When_UsernameIsPassed()
        {
            // Arrange
            var claims = factory.CreateAdminClaims();
            claims.Add(new Claim("username", "Admin"));
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resourceName = "read";

            // Act
            var actual = transformer.Authenticate(resourceName, principal);
            var expected = typeof(ClaimsPrincipal);

            // Assert
            Assert.IsType(expected, actual);
        }
    }
}
