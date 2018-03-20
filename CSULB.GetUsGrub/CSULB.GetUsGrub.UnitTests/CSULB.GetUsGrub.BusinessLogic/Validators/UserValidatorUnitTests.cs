using CSULB.GetUsGrub.BusinessLogic;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>UserValidatorUnitTests</c> class.
    /// Contains unit tests for UserValidator.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/13/2018
    /// </para>
    /// </summary>
    public class UserValidatorUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_UsernameDoesNotEqualDisplayName()
        {
            // Arrange
            var userValidator = new UserValidator();
            var username = "username";
            var displayName = "displayname";

            // Act
            var result = userValidator.CheckIfUsernameEqualsDisplayName(username, displayName);

            // Assert
            result.Data.Should().Be(false);
        }
    }
}
