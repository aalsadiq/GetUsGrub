using CSULB.GetUsGrub.BusinessLogic;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class TokenValidator
    {
        TokenValidator tokenValidator = new TokenValidator();

        [Fact]
        public void Should_NotThrowException_When_JwtIsValid()
        {
            // Arrange
            var token = "";
            var key = "";
            var jwt = new TokenService().GetJwtSecurityToken(token);

            // 
        }
    }
}
