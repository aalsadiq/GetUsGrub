using CSULB.GetUsGrub.BusinessLogic;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests.CSULB.GetUsGrub.BusinessLogic.Services
{
    /// <summary>
    /// The <c>PayloadHasherUnitTests</c> class.
    /// Contains unit tests for PayloadHasher.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/13/2018
    /// </para>
    /// </summary>
    public class PayloadHasherUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_TwoHashesAreEqualGivenSameSaltAndPayload()
        {
            // Arrange
            var payloadHasher = new PayloadHasher();
            var salt = "ThisIsTheSalt!@#123";
            var payload = "ThisIsThePayload";

            // Act
            var firstHash = payloadHasher.Sha256HashWithSalt(salt, payload);
            var secondHash = payloadHasher.Sha256HashWithSalt(salt, payload);

            // Assert
            firstHash.Should().Match(secondHash);
        }

        [Fact]
        public void Should_PassValidation_When_TwoHashesAreNotEqualGivenDifferentSaltAndSamePayload()
        {
            // Arrange
            var payloadHasher = new PayloadHasher();
            var firstSalt = "ThisIsTheSalt!@#123";
            var secondSalt = "ThisIsADifferentSalt$%^";
            var payload = "ThisIsThePayload";

            // Act
            var firstHash = payloadHasher.Sha256HashWithSalt(firstSalt, payload);
            var secondHash = payloadHasher.Sha256HashWithSalt(secondSalt, payload);

            // Assert
            firstHash.Should().NotMatch(secondHash);
        }

        [Fact]
        public void Should_PassValidation_When_TwoHashesAreNotEqualGivenSameSaltAndDifferentPayload()
        {
            // Arrange
            var payloadHasher = new PayloadHasher();
            var salt = "ThisIsTheSalt!@#123";
            var firstPayload = "ThisIsThePayload";
            var secondPayload = "ThisIsADifferentPayload%^&";

            // Act
            var firstHash = payloadHasher.Sha256HashWithSalt(salt, firstPayload);
            var secondHash = payloadHasher.Sha256HashWithSalt(salt, secondPayload);

            // Assert
            firstHash.Should().NotMatch(secondHash);
        }

        [Fact]
        public void Should_ReturnTrue_When_RawSignatureIsSameAsSignature()
        {
            // Arrange
            var payloadHasher = new PayloadHasher();
            var secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
            var rawSignature = "kNOwv3BKRkVXoJz4NSuOo_qxsPC9ltQw1oX_RBPrqgU";
            var headerAndPayload = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InVzZXJuYW1lMDEiLCJwYXNzd29yZCI6InBhc3N3b3JkMDEiLCJhcHBsaWNhdGlvbiI6IkdldFVzR3J1YiIsInJvbGVUeXBlIjoicHVibGljIiwiaWF0IjoxNTE2MjM5MDIyfQ";

            // Act
            var signature = payloadHasher.Sha256HashWithSalt(salt: secret, payload: headerAndPayload);
            var result = (signature == rawSignature);

            // Assert
            signature.Should().Be("asdasdas");
            result.Should().BeTrue();
        }
    }
}
