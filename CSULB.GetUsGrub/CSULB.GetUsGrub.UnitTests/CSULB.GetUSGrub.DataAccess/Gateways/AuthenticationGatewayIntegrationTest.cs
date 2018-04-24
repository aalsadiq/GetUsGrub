using System;
using CSULB.GetUsGrub.DataAccess;
using Xunit;
using FluentAssertions;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.UnitTests
{
    public class AuthenticationGatewayIntegrationTest
    {
        [Fact]
        public void Should_ReturnFailedAttempts_When_UserIsValid_And_HasAttemptedLogin()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var username = "batman";

            // Act
            var result = gateway.GetFailedAttempt(username);

            // Assert
            result.Data.Id.Should().NotBeNull();
        }

        [Fact]
        public void Should_ReturnNull_When_UserIsInvalid_Or_NeverAttemptedLogin()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var username = "NotUser";

            // Act
            var result = gateway.GetFailedAttempt(username);

            // Assert
            result.Data.Should().BeNull();
        }

        [Fact]
        public void Should_ReturnAuthenticationToken_When_UserIsValid()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var username = "batman";
            
            // Act
            var result = gateway.GetAuthenticationToken(username);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().NotBeNull();
        }

        [Fact]
        public void Should_ReturnNull_When_UserIsInvalid_NotAuthenticationToken()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var username = "notuser";

            // Act
            var result = gateway.GetAuthenticationToken(username);

            //Assert
            result.Data.Should().BeNull();
        }

        [Fact]
        public void Should_ReturnUserAccount_When_UserIsValid()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var username = "batman";
           
            // Act
            var result = gateway.GetUserAccount(username);

            // Assert
            result.Data.Should().NotBeNull();
        }

        [Fact]
        public void Should_ReturnNull_When_UserIsInvalid_NotUserAccount()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var username = "NotUser";

            //Act
            var result = gateway.GetUserAccount(username);

            //Assert
            result.Data.Should().BeNull();
        }

        [Fact]
        public void Should_ReturnPasswordSalt_When_UserIdIsValid()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var userId = 51;

            // Act
            var result = gateway.GetUserPasswordSalt(userId);

            // Assert
            result.Data.Should().BeOfType<PasswordSalt>();
        }

        
        [Fact]
        public void Should_ReturnNull_When_UserIdIsInvalid()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var userId = 500;

            // Act
            var result = gateway.GetUserPasswordSalt(userId);

            // Assert
            result.Data.Salt.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnTrue_When_UserNameIsValid_InToken()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var incomingAuthenticationToken = new AuthenticationToken()
            {
                Username = "batman",
                TokenString = "TokenString",
                ExpiresOn = DateTime.UtcNow
            };

            // Act
            var result = gateway.StoreAuthenticationToken(incomingAuthenticationToken);

            // Assert
            result.Data.Should().BeTrue();
            result.Error.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnFalse_When_UserNameIsValid_NoExpirarionTime_InToken()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var incomingAuthenticationToken = new AuthenticationToken()
            {
                Username = "batman",
                TokenString = "TokenString",
            };

            // Act
            var result = gateway.StoreAuthenticationToken(incomingAuthenticationToken);

            // Assert
            result.Data.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnFalse_When_UserNameIsInvalid_InToken()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var incomingAuthenticationToken = new AuthenticationToken()
            {
                Username = "NotUser",
                TokenString = "TokenString"
            };

            // Act
            var result = gateway.StoreAuthenticationToken(incomingAuthenticationToken);

            // Assert
            result.Data.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnFalse_When_UserNameIsNull_InToken()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var incomingAuthenticationToken = new AuthenticationToken()
            {
                TokenString = "TokenString"
            };

            // Act
            var result = gateway.StoreAuthenticationToken(incomingAuthenticationToken);

            // Assert
            result.Data.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnTrue_When_UserIdIsValid_InFailedAttempt()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var incomingFailedAttempt = new FailedAttempts()
            {
                Id = 51,
                LastAttemptTime = DateTime.UtcNow
            };

            // Act
            var result = gateway.UpdateFailedAttempt(incomingFailedAttempt);

            // Assert
            result.Data.Should().BeTrue();
            result.Error.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnFalse_When_UserIdIsInvalid_InFailedAttempt()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var incomingFailedAttempt = new FailedAttempts()
            {
                Id = 500,
                LastAttemptTime = DateTime.UtcNow
            };

            // Act
            var result = gateway.UpdateFailedAttempt(incomingFailedAttempt);

            // Assert
            result.Data.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_ReturnTrue_When_UserIdIsNull_InFailedAttempt()
        {
            // Arrange
            var gateway = new AuthenticationGateway();
            var incomingFailedAttempt = new FailedAttempts()
            {
                LastAttemptTime = DateTime.UtcNow
            };

            // Act
            var result = gateway.UpdateFailedAttempt(incomingFailedAttempt);

            // Assert
            result.Data.Should().BeFalse();
            result.Error.Should().NotBeNullOrEmpty();
        }
    }
}
