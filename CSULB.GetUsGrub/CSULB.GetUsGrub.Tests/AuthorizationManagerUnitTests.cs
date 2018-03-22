using System.Security.Claims;
using CSULB.GetUsGrub.UserAccessControl;
using Xunit;

namespace CSULB.GetUsGrub.Tests
{
    public class AuthorizationManagerUnitTests
    {
        [Fact]
        public void CheckAccess_Success()
        {
            AuthorizationManager authorizationManager = new AuthorizationManager();
            ClaimsFactory factory = new ClaimsFactory();

            // Arrange
            var claims = factory.CreateAdminClaims();
            claims.Add(new Claim("username", "Admin"));
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.REACTIVATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var actual = authorizationManager.CheckAccess(context);
            var expected = true;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CheckAccess_Fail()
        {
            AuthorizationManager authorizationManager = new AuthorizationManager();
            ClaimsFactory factory = new ClaimsFactory();

            // Arrange
            var claims = factory.CreateIndividualClaims();
            claims.Add(new Claim("username", "Individual"));
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            var resource = ResourceConstant.USER;
            var action = ActionConstant.REACTIVATE;

            var context = new AuthorizationContext(principal, resource, action);

            // Act
            var actual = authorizationManager.CheckAccess(context);
            var expected = false;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
