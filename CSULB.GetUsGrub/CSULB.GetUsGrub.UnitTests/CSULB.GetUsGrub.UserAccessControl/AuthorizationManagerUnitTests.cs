using Xunit;
using FluentAssertions;
using System.Security;
using System.Security.Claims;
using CSULB.GetUsGrub.UserAccessControl;
using System.Collections.Generic;

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

        [Fact]
        public void Should_ReturnTrue_When_PrincipalHasClaim()
        {
            // Arrange
            var username = "username1";
            var claims = new List<Claim> { new Claim("username", username) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.PREFERENCES;
            var action = ActionConstant.UPDATE;

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
            var username = "username1";
            var claims = new List<Claim> { new Claim("username", username) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.UPDATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var result = manager.CheckAccess(context);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Should_ThrowException_When_PrincipalIsInvalid_EmptyUsername()
        {
            // Arrange
            string username = "";
            var claims = new List<Claim> { new Claim("username", username) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.UPDATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var result = Assert.Throws<SecurityException>(() => manager.CheckAccess(context)).Message;
            var expected = "Username is invalid.";

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void Should_ThrowException_When_PrincipalIsInvalid_NonexistentUsername()
        {
            // Arrange
            string username = "blah";
            var claims = new List<Claim> { new Claim("username", username) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.UPDATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var result = Assert.Throws<SecurityException>(() => manager.CheckAccess(context)).Message;
            var expected = "User is invalid.";

            // Assert
            result.Should().Be(expected);
        }
    }
}
