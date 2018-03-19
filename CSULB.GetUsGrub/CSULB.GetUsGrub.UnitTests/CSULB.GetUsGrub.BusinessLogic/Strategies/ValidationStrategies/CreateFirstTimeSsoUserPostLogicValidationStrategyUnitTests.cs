using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class CreateFirstTimeSsoUserPostLogicValidationStrategyUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_GivenValidUserAccountAndPasswordSalt()
        {
            // Arrange
            var userAccount = new UserAccount(username:"username", password:"!@#@$123asd!", isFirstTimeUser:true, roleType:"public");
            var passwordSalt = new PasswordSalt(salt:"@$!@#a2131asda@#!");
            var createFirstTimeSsoUserPostLogicValidationStrategy = new CreateFirstTimeSsoUserPostLogicValidationStrategy(userAccount, passwordSalt);

            // Act
            var result = createFirstTimeSsoUserPostLogicValidationStrategy.ExecuteStrategy();

            // Assert
            result.Data.Should().BeTrue();
            result.Error.Should().BeNull();
        }

        [Fact]
        public void Should_FailValidation_When_GivenEmptyUsername()
        {
            // Arrange
            var userAccount = new UserAccount(username: "", password: "!Q2w#E4r", isFirstTimeUser: false, roleType: "public");
            var passwordSalt = new PasswordSalt(salt: "@$!@#a2131asda@#!");
            var createFirstTimeSsoUserPostLogicValidationStrategy = new CreateFirstTimeSsoUserPostLogicValidationStrategy(userAccount, passwordSalt);

            // Act
            var result = createFirstTimeSsoUserPostLogicValidationStrategy.ExecuteStrategy();

            // Assert
            result.Data.Should().BeFalse();
            result.Error.Should().Be("Something went wrong. Please try again later.");
        }
    }
}
