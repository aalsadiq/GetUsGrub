using Xunit;
using FluentAssertions;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.UnitTests.CSULB.GetUsGrub.UserManagement.CRUD
{
    public class UserManagementTests
    {
        // Arrange
        public UserGateway userGateway = new UserGateway(); //Creating user gateway so all tests can use the gateway

        // Deactivate User
        [Fact]
        public void DeactivateUser_When_GivenUserName()
        {
            //Act
            var response = userGateway.DeactivateUser("username1");

            //Assert
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        // Reactivate User
        [Fact]
        public void Should_ReactivateUser_When_Given_UserName_User1()
        {
            //Act
            var response = userGateway.ReactivateUser("username2");

            //Assert
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        // Edit User
        [Fact]
        public void EditUserName_ValidEditUserName_Pass() //If you run twice, it will fail since user does not exist.
        {
            var response = userGateway.EditUserName("username5", "Five"); // username5 & Five
            response.Data.Should().BeTrue();
        }

        [Fact]
        public void EditUserName_InvalidEditUserName_Fail()
        {
            //Act
            var response = userGateway.EditUserName("InvalidUser", "shouldnotexist"); // username5 & Five

            //Assert
            response.Data.Should().BeFalse();
        }

        [Fact]
        public void EditDisplayName_ValidEditDisplayName_Pass()
        {
            //Act
            var response = userGateway.EditDisplayName("username6", "ValidUserName6");

            //Assert
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        [Fact]
        public void EditDisplayName_InvalidEditDisplayName_Fail()
        {
            //Act
            var response = userGateway.EditDisplayName("xuserx", "displayx");

            //Assert
            response.Data.Should().BeFalse();
            response.Error.Should().Be(GeneralErrorMessages.GENERAL_ERROR);
        }

        [Fact]
        public void EditUser_ValidEditUserName_Pass()
        {
            // Arrange
            var user = new EditUserDto()
            {
                Username = "username12",
                NewUsername = "12",
                NewDisplayName = ""
            };

            //Act
            var response = userGateway.EditUser(user);

            //Assert
            response.Data.Should().BeTrue();
        }

        [Fact]
        public void EditUser_validEditDisplayName_Pass()
        {
            // Arrange
            var user = new EditUserDto()
            {
                Username = "username13",
                NewUsername = "",
                NewDisplayName = "13DisplayName"
            };

            //Act
            var response = userGateway.EditUser(user);

            //Assert
            response.Data.Should().BeTrue();
        }

        // Deleting user
        [Fact]
        public void DeleteUser_ValidDelete_Pass()
        {
            // Act
            var response = userGateway.DeleteUser("username20");
            // Assert
            response.Data.Should().BeTrue();
        }

        [Fact]
        public void DeleteUser_ValidDelete_Fail()
        {
            // Act
            var response = userGateway.DeleteUser("username20"); // Deleteing the same user as above, should fail because it does not exist.

            // Assert
            response.Data.Should().BeFalse();
        }

    }
}
