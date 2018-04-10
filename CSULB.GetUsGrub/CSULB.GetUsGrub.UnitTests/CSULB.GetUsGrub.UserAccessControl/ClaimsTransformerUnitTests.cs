using Xunit;
using System.Security.Claims;
using CSULB.GetUsGrub.UserAccessControl;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit testing for the ClaimsTransformer.
    /// @author: Rachel Dang
    /// @updated: 04/09/2018
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
            var user = "username1";
            var claims = new List<Claim> { new Claim("Username", user) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resourceName = "permission";

            // Act
            principal = transformer.Authenticate(resourceName, principal);
            var result = principal.HasClaim(ActionConstant.UPDATE + ResourceConstant.PREFERENCES, "True");
            
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Should_ReturnClaimsPrincipal_With_ReadPermissions()
        {
            // Arrange
            var user = "username1";
            var claims = new List<Claim> { new Claim("Username", user) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resourceName = "read";

            // Act
            var allClaims = transformer.Authenticate(resourceName, principal).HasClaim(ActionConstant.UPDATE + ResourceConstant.PREFERENCES, "True");
            var readClaims = transformer.Authenticate(resourceName, principal).HasClaim(ActionConstant.READ + ResourceConstant.PREFERENCES, "True");

            // Assert
            allClaims.Should().BeFalse();
            readClaims.Should().BeTrue();
        }
    }
}
