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
     
        // Deactivate Username
        public string DeactivateUsername = "username3";
        [Fact]
        public void DeactivateUser_When_GivenUserName()
        {
            //Act
            var response = userGateway.DeactivateUser(DeactivateUsername);
            //Assert
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        // Reactivate Username
        public string ReactivateUsername = "username8";
        [Fact]
        public void Should_ReactivateUser_When_Given_UserName_User1()
        {
            //Act
            var response = userGateway.ReactivateUser(ReactivateUsername);
            //Assert
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        // Editing user
        public string UsernameToEdit = "U10";
        public string EditDisplayName = "NDN10";
        public string newUserName = "username10";
        [Fact]
        public void EditUserName_ValidEditUserName_Pass()//If you run twice, it will fail since user2 does not exist.
        {
            var response = userGateway.EditUserName(UsernameToEdit, newUserName);
            response.Data.Should().BeTrue();
        }

        [Fact]
        public void EditUserName_InvalidEditUserName_Fail()
        {
            //Act
            var response = userGateway.EditUserName(UsernameToEdit, newUserName);

            //Assert
            response.Data.Should().BeFalse();
        }

        //[Fact]
        //public void EditDisplayName_ValidEditDisplayName_Pass()
        //{
        //    //Act
        //    var response = userGateway.EditDisplayName(UsernameToEdit, DisplayName);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //    response.Error.Should().BeNull();
        //}

        //[Fact]
        //public void EditDisplayName_InvalidEditDisplayName_Fail()
        //{
        //    //Act
        //    var response = userGateway.EditDisplayName(InvalidUser, DisplayName);
        //    //Assert
        //    response.Data.Should().BeFalse();
        //    response.Error.Should().Be(GeneralErrorMessages.GENERAL_ERROR);
        //}

        //[Fact]
        //public void EditUser_ValidEditUserName_Pass()
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = UsernameToEdit,
        //        NewUsername = Username,
        //        NewDisplayName = DisplayName
        //    };
        //    //Act
        //    var response = userGateway.EditUser(user);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        //[Fact]
        //public void EditUser_validEditDisplayName_Pass()
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = UsernameToEdit,
        //        NewUsername = Username,
        //        NewDisplayName = DisplayName
        //    };
        //    //Act
        //    var response = userGateway.EditUser(user);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        public string DeleteUser = "username1";

        [Fact]
        public void DeleteUser_ValidDelete_Pass()
        {
            // Act
            var response = userGateway.DeleteUser(DeleteUser);
            // Assert
            response.Data.Should().BeTrue();
        }

        [Fact]
        public void DeleteUser_ValidDelete_Fail()
        {
            // Act
            var response = userGateway.DeleteUser(DeleteUser); // Deleteing the same user
            // Assert
            response.Data.Should().BeFalse();
        }

    }
}
