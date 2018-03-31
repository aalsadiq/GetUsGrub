using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class ValidationWrapperUnitTests
    {
        [Fact]
        public void Should_PassValidation_When_GivenValidUserAccountAndPasswordSalt()
        {
            // Arrange
            var userAccount = new UserAccount(username: "username", password: "!@#@$123asd!", isActive:false, isFirstTimeUser: true, roleType: "public");
            var validationWrapper = new ValidationWrapper<UserAccount>(data: userAccount, ruleSet: "SsoRegistration", validator: new UserAccountValidator());

            // Act
            var result = validationWrapper.ExecuteValidator();

            // Assert
            result.Data.Should().BeTrue();
            result.Error.Should().BeNull();
        }

        [Fact]
        public void Should_FailValidation_When_GivenEmptyUsername()
        {
            // Arrange
            var userAccount = new UserAccount(username: "", password: "!Q2w#E4r", isActive: false, isFirstTimeUser: false, roleType: "public");
            var validationWrapper = new ValidationWrapper<UserAccount>(data: userAccount, ruleSet: "SsoRegistration", validator: new UserAccountValidator());

            // Act
            var result = validationWrapper.ExecuteValidator();

            // Assert
            result.Data.Should().BeFalse();
        }
    }
}
