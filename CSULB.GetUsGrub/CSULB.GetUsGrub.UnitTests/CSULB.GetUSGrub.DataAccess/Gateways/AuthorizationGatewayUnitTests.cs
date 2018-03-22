using CSULB.GetUsGrub.DataAccess;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;
using FluentAssertions;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit texting for the AuthorizationGateway.
    /// @author: Rachel Dang
    /// @updated: 03/22/2018
    /// </summary>
    public class AuthorizationGatewayUnitTests
    {
        //// Should be a fail since user doesn't exist
        //[Fact]
        //public void Should_ReturnClaims_When_UsernameIsValid()
        //{
        //    // Arrange
        //    var gateway = new AuthorizationGateway();
        //    var username = "User1";
        //    var expected = typeof(List<Claim>);

        //    // Act
        //    var actual = gateway.GetClaimsByUsername(username);

        //    // Assert
        //    Assert.IsType(expected, actual.Data);
        //    Assert.Null(actual.Error);
        //}

        // Should be a pass since user doesn't exist
        [Fact]
        public void Should_ReturnError_When_UserIsInvalid()
        {
            // Arrange
            var gateway = new AuthorizationGateway();
            var username = "User1";
            var expected = typeof(List<Claim>);

            // Act
            var result = gateway.GetClaimsByUsername(username);

            // Assert
            result.Should().BeOfType(expected);
            result.Error.Should().NotBeNull();
        }
    }
}
