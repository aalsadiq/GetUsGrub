using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.UserAccessControl;
using FluentAssertions;
using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit testing for the AuthorizationManager.
    /// @author: Rachel Dang
    /// @updated: 04/24/2018
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
            var claims = new List<Claim> { new Claim("Username", username) };
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
            var claims = new List<Claim> { new Claim("Username", username) };
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
            var claims = new List<Claim> { new Claim("Username", username) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.UPDATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var result = Assert.Throws<SecurityException>(() => manager.CheckAccess(context)).Message;

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void Should_ThrowException_When_PrincipalIsInvalid_NonexistentUsername()
        {
            // Arrange
            string username = "blah";
            var claims = new List<Claim> { new Claim("Username", username) };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.UPDATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var result = Assert.Throws<SecurityException>(() => manager.CheckAccess(context)).Message;

            // Assert
            result.Should().NotBeNull();
        }
    }
}
