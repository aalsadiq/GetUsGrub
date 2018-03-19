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
            var firstHash = payloadHasher.Sha256HashWithSaltBase64(firstSalt, payload);
            var secondHash = payloadHasher.Sha256HashWithSaltBase64(secondSalt, payload);

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
            var firstHash = payloadHasher.Sha256HashWithSaltBase64(salt, firstPayload);
            var secondHash = payloadHasher.Sha256HashWithSaltBase64(salt, secondPayload);

            // Assert
            firstHash.Should().NotMatch(secondHash);
        }
    }
}
