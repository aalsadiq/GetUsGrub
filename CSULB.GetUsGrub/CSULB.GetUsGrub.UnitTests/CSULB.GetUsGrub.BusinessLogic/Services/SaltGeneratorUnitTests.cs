using CSULB.GetUsGrub.BusinessLogic;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>SaltGeneratorUnitTests</c> class.
    /// Contains unit tests for SaltGenerator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/13/2018
    /// </para>
    /// </summary>
    public class SaltGeneratorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_TwoGeneratedSaltsAreNotEqualGivenSameSaltSize()
        {
            // Arrange
            var saltGenerator = new SaltGenerator();

            // Act
            var firstSalt = saltGenerator.GenerateSalt(128);
            var secondSalt = saltGenerator.GenerateSalt(128);

            // Assert
            firstSalt.Should().NotMatch(secondSalt);
        }
    }
}
