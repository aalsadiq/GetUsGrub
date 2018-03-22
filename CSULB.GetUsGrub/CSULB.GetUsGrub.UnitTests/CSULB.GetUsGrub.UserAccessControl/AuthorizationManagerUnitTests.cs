using Xunit;
using System.Security.Claims;
using CSULB.GetUsGrub.UserAccessControl;
using System;

using System.Security;


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
            var actual = manager.CheckAccess(context);
            var expected = true;

            // Assert
            Assert.Equal(expected, actual);
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
            var actual = manager.CheckAccess(context);
            var expected = false;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Should_ThrowException_When_PrincipalIsInvalid()
        {
            var claims = factory.CreateIndividualClaims();
            claims.Add(new Claim("username", ""));
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.REACTIVATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            Exception ex = Assert.Throws<SecurityException>(() => manager.CheckAccess(context));
            var actual = ex.Message;
            var expected = "Username is invalid.";

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
