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

        //[Fact]
        //public void DeactivateUser_When_GivenUserName()
        //{
        //    //Act
        //    var response = userGateway.DeactivateUser("Username1");
        //    //Assert
        //    response.Data.Should().BeTrue();
        //    response.Error.Should().BeNull();
        //}

        //[Fact]
        //public void Should_ReactivateUser_When_Given_UserName_User1()
        //{
        //    //Act
        //    var response = userGateway.ReactivateUser("Username2");
        //    //Assert
        //    response.Data.Should().BeTrue();
        //    response.Error.Should().BeNull();
        //}

        //[Fact]
        //public void EditUserName_ValidEditUserName_Pass()//If you run twice, it will fail since user2 does not exist.
        //{
        //    var response = userGateway.EditUserName("User2", "NewUser2");//because there is no user1 anymore... it's NewUser1
        //    response.Data.Should().BeTrue();
        //    response.Error.Should().BeNull();
        //}

        //[Fact]
        //public void EditUserName_InvalidEditUserName_Fail()
        //{
        //    //Act
        //    var response = userGateway.EditUserName("User2", "User2");
        //    //Assert
        //    response.Data.Should().BeFalse();
        //    //response.Error.Should().Be();//Not null
        //}
        //[Fact]
        //public void EditDisplayName_ValidEditDisplayName_Pass()
        //{
        //    //Act
        //    var response = userGateway.EditDisplayName("User3", "NewDisplayName3");
        //    //Assert
        //    response.Data.Should().BeTrue();
        //    response.Error.Should().BeNull();
        //}
        //[Fact]
        //public void EditDisplayName_InvalidEditDisplayName_Fail()
        //{
        //    //Act
        //    var response = userGateway.EditDisplayName("UserDoesNotExist", "DisplayName4");
        //    //Assert
        //    response.Data.Should().BeFalse();
        //    response.Error.Should().Be("Something went wrong. Please try again later.");
        //}

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

        //[Fact]
        //public void EditUser_ValidEditUserPassword_Pass()//Will fail if you run twice because the new password cannot be the same as the current password.
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = "User9",
        //        NewUsername = "User9",
        //        NewDisplayName = "DisplayName9",
        //        NewPassword = "EditUserPassword10"//Will change this value in the database
        //    };
        //    //Act
        //    var response = userGateway.EditUser(user);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        //[Fact]
        //public void EditUser_ValidEditUserName_Pass()//Will fail if you run twice because the new username cannot be the same as the current username.
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = "User8",//No change
        //        NewUsername = "EditUsername8",//Change
        //        NewDisplayName = "DisplayName8",//No change
        //        NewPassword = "password123!@"//No change
        //    };
        //    //Act
        //    var response = userGateway.EditUser(user);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        //[Fact]
        //public void EditUser_validEditDisplayName_Pass()//Will fail if you run twice because the new  displayname cannot be the same as the current display name.
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = "User6",//No change
        //        NewUsername = "Username6",//Change
        //        NewDisplayName = "NewDisplayName6",//Change
        //        NewPassword = "password123!@"//No change
        //    };
        //    //Act
        //    var response = userGateway.EditUser(user);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

         //Testing edit user
        //[Fact]
        //public void EditUser_validEditDisplayName_Pass()//Will fail if you run twice because the new  displayname cannot be the same as the current display name.
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = "User5",//No change
        //        NewUsername = "EditUser5",//Change
        //        NewDisplayName = "EditUserDisplayName5",//Change
        //    };
        //    //Act
        //    var response = userGateway.EditUser(user);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        [Fact]
        public void DeleteUser_ValidDelete_Pass()//If you run twice, it should fail since user1 does not exist!
        {
            //Assert
            var response = userGateway.DeleteUser("username27");
            response.Data.Should().BeTrue();
        }

        //[Fact] 
        //public void DeleteSecurityQuestionByUsernam_ValidDelete_Pass()
        //{
        //    //Assert
        //    var response = userGateway.DeleteSecurityQuestionByUsername("Gaby");
        //    response.Data.Should().BeTrue();
        //}
        //[Fact]
        //public void DeleteUser_ValidDelete_Fail()
        //{
        //    //Assert
        //    var response = userGateway.DeleteUser("Gaby");
        //    response.Data.Should().BeFalse();
        //}

    }
}
