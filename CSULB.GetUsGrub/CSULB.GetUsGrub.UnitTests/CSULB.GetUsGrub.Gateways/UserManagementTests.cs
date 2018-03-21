using CSULB.GetUsGrub.DataAccess;
using Xunit;
using FluentAssertions;

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
        //    var response = userGateway.DeactivateUser("User10");
        //    //Assert
        //    response.Data.Should().BeTrue();
        //    response.Error.Should().BeNull();
        //}

        //[Fact]
        //public void Should_ReactivateUser_When_Given_UserName_User1()
        //{
        //    //Act
        //    var response = userGateway.ReactivateUser("User4");
        //    //Assert
        //    response.Data.Should().BeTrue();
        //    response.Error.Should().BeNull();
        //}

        ////[Fact]
        ////public void EditUserName_ValidEditUserName_Pass()//Fail...-It's okay thought! (should only have one fail)
        ////{
        ////    var response = userGateway.EditUserName("User2", "NewUser2");//because there is no user1 anymore... it's NewUser1
        ////    response.Data.Should().BeTrue();
        ////    response.Error.Should().BeNull();
        ////}

        //[Fact]
        //public void EditUserName_InvalidEditUserName_Fail()
        //{
        //    //Act
        //    var response = userGateway.EditUserName("User11", "User5");
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
        //    var response = userGateway.EditDisplayName("User11", "DisplayName4");
        //    //Assert
        //    response.Data.Should().BeFalse();
        //    response.Error.Should().Be("Something went wrong. Please try again later.");
        //}

        //[Fact]
        //public void ResetPassword_ValidResetPassword_Pass()
        //{
        //    //Act
        //    var response = userGateway.ResetPassword("User1", "NewPassword1!");
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        //[Fact]
        //public void ResetPassword_InvalidResetPassword_Fail()
        //{
        //    //Act
        //    var response = userGateway.ResetPassword("User11", "Password123!@");
        //    //Assert
        //    response.Data.Should().BeFalse();
        //    response.Error.Should().Be("Something went wrong. Please try again later.");
        //}

        //////It will fail since it will not find EditUserName8Twice, because it has already changed...

        //[Fact]
        //public void EditUser_ValidEditUserPassword_Pass()//does not like this...
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = "User10",
        //        NewUsername = "User10",
        //        NewDisplayName = "DisplayName10",
        //        NewPassword = "EditUserPassword10"
        //    };
        //    //Act
        //    var response = userGateway.EditUser(user);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        //[Fact]
        //public void EditUser_ValidEditUserName_Pass()
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = "User7",//No change
        //        NewUsername = "NewUserName7",//Change
        //        NewDisplayName = "DisplayName7",//No change
        //        NewPassword = "password123!@"//No change
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
        //        Username = "User6",//No change
        //        NewUsername = "Username6",//Change
        //        NewDisplayName = "NewDisplayName6",//No change
        //        NewPassword = "password123!@"//No change
        //    };
        //    //Act
        //    var response = userGateway.EditUser(user);
        //    //Assert
        //    response.Data.Should().BeTrue();
        //}

        [Fact]
        public void DeleteUser_ValidDelete_Pass()
        {
            //Assert
            var response = userGateway.DeleteUser("User3");
            response.Data.Should().BeTrue();
        }

        //[Fact]
        //public void DeleteUser_ValidDelete_Fail()
        //{
        //    //Assert
        //    var response = userGateway.DeleteUser("User100");
        //    response.Data.Should().BeFalse();
        //}

    }
}
