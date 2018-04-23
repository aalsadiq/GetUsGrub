using Xunit;
using FluentAssertions;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.UnitTests.CSULB.GetUsGrub.UserManagement.CRUD
{
    public class UserManagementTests
    {
        //Arrange
        public UserGateway userGateway = new UserGateway();//Creating user gateway so all tests can use the gateway

        public string DeactivateUsername = "username3";
        public string ReactivateUsername = "username8";

        public string InvalidUser = "DNE";
        public string UsernameToEdit = "username10";
        public string Username = "newusername10";
        public string DisplayName = "newdisplayname10";

        public string EditUsername = "username10";
        public string EditDisplayName = "newdisplaname";
        public string DeleteUser = "username25";
        


        [Fact]
        public void DeactivateUser_When_GivenUserName()
        {
            //Act
            var response = userGateway.DeactivateUser(DeactivateUsername);
            //Assert
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        [Fact]
        public void Should_ReactivateUser_When_Given_UserName_User1()
        {
            //Act
            var response = userGateway.ReactivateUser(ReactivateUsername);
            //Assert
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        [Fact]
        public void EditUserName_ValidEditUserName_Pass()//If you run twice, it will fail since user2 does not exist.
        {
            var response = userGateway.EditUserName(UsernameToEdit, Username);//because there is no user1 anymore... it's NewUser1
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        [Fact]
        public void EditUserName_InvalidEditUserName_Fail()
        {
            //Act
            var response = userGateway.EditUserName(UsernameToEdit, UsernameToEdit);

            //Assert
            response.Data.Should().BeFalse();
        }

        [Fact]
        public void EditDisplayName_ValidEditDisplayName_Pass()
        {
            //Act
            var response = userGateway.EditDisplayName(UsernameToEdit, DisplayName);
            //Assert
            response.Data.Should().BeTrue();
            response.Error.Should().BeNull();
        }

        [Fact]
        public void EditDisplayName_InvalidEditDisplayName_Fail()
        {
            //Act
            var response = userGateway.EditDisplayName(InvalidUser, DisplayName);
            //Assert
            response.Data.Should().BeFalse();
            response.Error.Should().Be(GeneralErrorMessages.GENERAL_ERROR);
        }

        //[Fact]
        //public void ResetPassword_ValidResetPassword_Pass()
        //{
        //    //Act
        //    var response = userGateway.ResetPassword("User3", "NewPassword3!");
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        //[Fact]
        //public void ResetPassword_InvalidResetPassword_Fail()
        //{
        //    //Act
        //    var response = userGateway.ResetPassword("UserDoesNotExist", "Password123!@");
        //    //Assert
        //    response.Data.Should().BeFalse();
        //    response.Error.Should().Be("Something went wrong. Please try again later.");
        //}

        [Fact]
        public void EditUser_ValidEditUserPassword_Pass()
        {
            var user = new EditUserDto()
            {
                Username = UsernameToEdit,
                NewUsername = Username,
                NewDisplayName = DisplayName
            };
            //Act
            var response = userGateway.EditUser(user);
            //Assert
            response.Data.Should().BeTrue();
        }

        [Fact]
        public void EditUser_ValidEditUserName_Pass()
        {
            var user = new EditUserDto()
            {
                Username = UsernameToEdit,
                NewUsername = Username,
                NewDisplayName = DisplayName
            };
            //Act
            var response = userGateway.EditUser(user);
            //Assert
            response.Data.Should().BeTrue();
        }

        [Fact]
        public void EditUser_validEditDisplayName_Pass()
        {
            var user = new EditUserDto()
            {
                Username = UsernameToEdit,
                NewUsername = Username,
                NewDisplayName = DisplayName
            };
            //Act
            var response = userGateway.EditUser(user);
            //Assert
            response.Data.Should().BeTrue();
        }

        [Fact]
        public void DeleteUser_ValidDelete_Pass()
        {
            //Assert
            var response = userGateway.DeleteUser(DeleteUser);
            response.Data.Should().BeTrue();
        }


        [Fact]
        public void DeleteUser_ValidDelete_Fail()
        {
            //Assert
            var response = userGateway.DeleteUser(DeleteUser);
            response.Data.Should().BeFalse();
        }

    }
}
