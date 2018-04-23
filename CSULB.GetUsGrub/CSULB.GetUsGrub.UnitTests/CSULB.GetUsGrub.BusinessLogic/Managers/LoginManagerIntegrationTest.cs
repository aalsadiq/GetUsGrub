using System;
using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using Xunit;
using FluentAssertions;

namespace CSULB.GetUsGrub.UnitTests
{
    public class LoginManagerIntegrationTest
    {
        [Fact]
        public void Should_ReturnLoginDtoResponse_When_UsernameAndPasswordAreValid()
        {
            // Arrange
            var manager = new LoginManager();
            var loginDto = new LoginDto("batman", "ASDqwegsdfASDfaergdfgerh");

            // Act
            var result = manager.LoginUser(loginDto);

            // Assert
            result.Data.Should().NotBeNull();
            result.Error.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnError_When_UsernameIsValid_AndPasswordIsInvalid()
        {
            // Arrange
            var manager = new LoginManager();
            var loginDto = new LoginDto("batman", "SDqwegsdfASDfaergdfgerh");

            // Act
            var result = manager.LoginUser(loginDto);

            // Assert
            result.Data.Should().BeNull();
            result.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnError_When_UsernameDoesNotExists()
        {
            // Arrange
            var manager = new LoginManager();
            var loginDto = new LoginDto("bruce", "ASDqwegsdfASDfaergdfgerh");

            // Act
            var result = manager.LoginUser(loginDto);

            // Assert
            result.Data.Should().BeNull();
            result.Error.Should().NotBeNullOrEmpty();
        }
    }
}
