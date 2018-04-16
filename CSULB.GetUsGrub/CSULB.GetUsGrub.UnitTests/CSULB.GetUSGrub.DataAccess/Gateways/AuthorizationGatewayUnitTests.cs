using CSULB.GetUsGrub.DataAccess;
using System.Security.Claims;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit texting for the AuthorizationGateway.
    /// @author: Rachel Dang
    /// @updated: 03/22/2018
    /// </summary>
    public class AuthorizationGatewayUnitTests
    {
        [Fact]
        public void Should_ReturnClaims_When_UserIsValid()
        {
            // Arrange
            var gateway = new AuthorizationGateway();
            var username = "username1";

            // Act
            var result = gateway.GetClaimsByUsername(username);

            // Assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Error.Should().BeNull();
        }

        [Fact]
        public void Should_ReturnError_When_UserIsInvalid()
        {
            // Arrange
            var gateway = new AuthorizationGateway();
            var username = "FailUser";

            // Act
            var result = gateway.GetClaimsByUsername(username);

            // Assert
            result.Data.Should().BeNull();
            result.Error.Should().NotBeNull();
        }
    }
}
