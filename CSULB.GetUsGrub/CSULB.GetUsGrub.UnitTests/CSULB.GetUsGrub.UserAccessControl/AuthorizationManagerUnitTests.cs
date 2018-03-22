using Xunit;
using FluentAssertions;
using System;
using System.Security;
using System.Security.Claims;
using CSULB.GetUsGrub.UserAccessControl;
namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit testing for the AuthorizationManager.
    /// @author: Rachel Dang
    /// @updated: 03/22/2018
    /// </summary>
    public class AuthorizationManagerUnitTests
    {
        // Arrange
        AuthorizationManager manager = new AuthorizationManager();
        ClaimsFactory factory = new ClaimsFactory();

        [Fact]
        public void Should_ReturnTrue_When_PrincipalHasClaim()
        {
            // Arrange
            var claims = factory.CreateAdminClaims();
            claims.Add(new Claim("username", "Admin"));
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.REACTIVATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var result = manager.CheckAccess(context);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Should_ReturnFalse_When_PrincipalDoesNotHaveClaim()
        {
            // Arrange
            var claims = factory.CreateIndividualClaims();
            claims.Add(new Claim("username", "Individual"));
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.REACTIVATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var result = manager.CheckAccess(context);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Should_ThrowException_When_PrincipalIsInvalid()
        {
            // Arrange
            var claims = factory.CreateIndividualClaims();
            claims.Add(new Claim("username", ""));
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.REACTIVATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var result = Assert.Throws<SecurityException>(() => manager.CheckAccess(context)).Message;
            var expected = "Username is invalid.";

            // Assert
            result.Should().Be(expected);
        }
    }
}
