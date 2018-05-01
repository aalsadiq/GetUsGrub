using Xunit;
using System.Security.Claims;
using CSULB.GetUsGrub.UserAccessControl;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit testing for the ClaimsTransformer.
    /// @author: Rachel Dang
    /// @updated: 04/24/2018
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
            var claims = new List<Claim> { new Claim(ResourceConstant.USERNAME, user) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resourceName = ResourceConstant.PREFERENCES;

            // Act
            principal = transformer.Authenticate(resourceName, principal);
            var result = principal.HasClaim(ActionConstant.UPDATE + ResourceConstant.PREFERENCES, "True");
            
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Should_ReturnClaimsIdentity_With_ReadPermissions()
        {
            // Arrange
            var username = "username1";
            var claims = new List<Claim> { new Claim(ResourceConstant.USERNAME, username) };
            var identity = new ClaimsIdentity(claims);

            // Act
            var result = transformer.CreateAuthenticationClaimsIdentity(username);

            // Assert
            var readClaim = result.HasClaim(ActionConstant.READ + ResourceConstant.PREFERENCES, "True");
            var notReadClaim = result.HasClaim(ActionConstant.UPDATE + ResourceConstant.PREFERENCES, "True");

            // Assert
            readClaim.Should().BeTrue();
            notReadClaim.Should().BeFalse();
        }

        [Fact]
        public void Should_ReturnClaimsIdentity_With_FirstTimeUserPermissions()
        {
            // Arrange
            var username = "ssoUser";

            // Act
            var claimsIdentity = transformer.CreateSsoClaimsIdentity(username);
            var result = claimsIdentity.HasClaim(ActionConstant.READ + ResourceConstant.FIRSTTIMEUSER, "True");

            // Assert
            result.Should().BeTrue();
        }
    }
}
