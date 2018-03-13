using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>UserAccountMapperUnitTests</c> class.
    /// Contains unit tests for UserAccountMapper.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserAccountMapperUnitTests
    {
        [Fact]
        public void Should_MapModelToDto_When_AllFieldsFromModelAreMappedToDto()
        {
            // Arrange
            var userAccount = new UserAccount()
            {
                Id = 0,
                Username = "username",
                Password = "!Q2w#E4r",
                IsActive = true,
                IsFirstTimeUser = true
            };
            var userAccountMapper = new UserAccountMapper();

            // Act
            var userAccountDto = userAccountMapper.MapModelToDto(userAccount);

            // Assert
            userAccountDto.Username.Should().Be("username");
            userAccountDto.Password.Should().Be("!Q2w#E4r");
            userAccountDto.IsActive.Should().Be(true);
            userAccountDto.IsFirstTimeUser.Should().Be(true);
        }

        [Fact]
        public void Should_MapDtoToModel_When_AllFieldsFromDtoAreMappedToModel()
        {
            // Arrange
            var userAccountDto = new UserAccountDto()
            {
                Username = "username",
                Password = "!Q2w#E4r",
                IsActive = true,
                IsFirstTimeUser = true
            };
            var userAccountMapper = new UserAccountMapper();

            // Act
            var userAccount = userAccountMapper.MapDtoToModel(userAccountDto);

            // Assert
            userAccount.Id.Should().Be(null);
            userAccount.Username.Should().Be("username");
            userAccount.Password.Should().Be("!Q2w#E4r");
            userAccount.IsActive.Should().Be(true);
            userAccount.IsFirstTimeUser.Should().Be(true);
        }
    }
}
