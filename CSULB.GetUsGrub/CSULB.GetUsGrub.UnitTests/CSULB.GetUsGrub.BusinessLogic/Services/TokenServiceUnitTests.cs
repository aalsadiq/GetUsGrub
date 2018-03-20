//using CSULB.GetUsGrub.BusinessLogic;
//using FluentAssertions;
//using Xunit;
// TODO: @Jenn Finish this [-Jenn]
//namespace CSULB.GetUsGrub.UnitTests
//{
//    public class TokenServiceUnitTests
//    {
//        private readonly TokenService _tokenService = new TokenService();

//        [Fact]
//        public void Should_ReturnTrue_When_InputsOfValidateSignatureAreValid()
//        {
//            // Arrange
//            var secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
//            var header = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
//            var payload = "eyJ1c2VybmFtZSI6InVzZXJuYW1lMDEiLCJwYXNzd29yZCI6InBhc3N3b3JkMDEiLCJhcHBsaWNhdGlvbiI6IkdldFVzR3J1YiIsInJvbGVUeXBlIjoicHVibGljIiwiaWF0IjoxNTE2MjM5MDIyfQ";
//            var rawSignature = "kNOwv3BKRkVXoJz4NSuOo_qxsPC9ltQw1oX_RBPrqgU";

//            // Act
//            var result = _tokenService.ValidateSignature(secret, header, payload, rawSignature);

//            // Assert
//            result.Should().BeTrue();
//        }
//    }
//}
