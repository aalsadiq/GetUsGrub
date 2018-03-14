using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>UserProfileMapperUnitTests</c> class.
    /// Contains unit tests for UserProfileMapper.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserProfileMapperUnitTests
    {
        [Fact]
        public void Should_MapModelToDto_When_AllFieldsFromModelAreMappedToDto()
        {
            // Arrange
            var userProfile = new UserProfile()
            {
                Id = 0,
                DisplayPicture = "www.img.com",
                DisplayName = "displayname"
            };
            var userProfileMapper = new UserProfileMapper();

            // Act
            var userProfileDto = userProfileMapper.MapModelToDto(userProfile);

            // Assert
            userProfileDto.DisplayPicture.Should().Be("www.img.com");
            userProfileDto.DisplayName.Should().Be("displayname");
        }

        [Fact]
        public void Should_MapDtoToModel_When_AllFieldsFromDtoAreMappedToModel()
        {
            // Arrange
            var userProfileDto = new UserProfileDto()
            {
                DisplayPicture = "www.img.com",
                DisplayName = "displayname"
            };
            var userProfileMapper = new UserProfileMapper();

            // Act
            var userProfile = userProfileMapper.MapDtoToModel(userProfileDto);

            // Assert
            userProfile.Id.Should().Be(null);
            userProfile.DisplayPicture.Should().Be("www.img.com");
            userProfile.DisplayName.Should().Be("displayname");
        }
    }
}
