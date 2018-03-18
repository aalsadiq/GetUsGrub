using CSULB.GetUsGrub.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CSULB.GetUsGrub.Models.DTOs;

namespace CSULB.GetUsGrub.UnitTests.CSULB.GetUsGrub.UserManagement.CRUD
{
    public class UserManagementTests
    {
        //Arrange
        public UserGateway userGateway = new UserGateway();//Creating user gateway so all tests can use the gateway

        //[Fact]
        //public void DeactivateUser_When_GivenUserNameUser1()
        //{
        //    //Act
        //    var result =  userGateway.DeactivateUser("User10");

        //    //Assert
        //    Assert.True(result);
        //}

        //[Fact]
        //public void Should_DeactivateUser_When_Given_UserName_User2()
        //{
        //    //Act
        //    var result = userGateway.DeactivateUser("User9");
        //    //Assert
        //    Assert.True(result);
        //}

        //[Fact]
        //public void Should_ReactivateUser_When_Given_UserName_User1()
        //{
        //    //Act
        //    var result = userGateway.ReactivateUser("User10");
        //    //Assert
        //    Assert.True(result);
        //}

        ////[Fact]
        ////public void EditUserName_ValidEditUserName_Pass()//Fail...-It's okay thought! (should only have one fail)
        ////{
        ////    var result = userGateway.EditUserName("User1", "NewUser1");//because there is no user1 anymore... it's NewUser1
        ////    Assert.True(result);
        ////}

        //[Fact]
        //public void EditUserName_InvalidEditUserName_Fail()
        //{
        //    //Act
        //    var result = userGateway.EditUserName("User5", "User5");
        //    //Assert
        //    result.Should().Be(true);
        //}
        //[Fact]
        //public void EditDisplayName_ValidEditDisplayName_Pass()
        //{
        //    //Act
        //    var result = userGateway.EditDisplayName("User3", "NewDisplayName3");
        //    //Assert
        //    result.Should().Be(true);
        //}
        //[Fact]
        //public void EditDisplayName_InvalidEditDisplayName_Fail()
        //{
        //    //Act
        //    var result = userGateway.EditDisplayName("User4", "DisplayName4");
        //    //Assert
        //    result.Should().Be(true);
        //}

        //[Fact]
        //public void ResetPassword_ValidResetPassword_Pass()
        //{
        //    //Act
        //    var result = userGateway.ResetPassword("User4", "NewPassword4!");
        //    //Assert
        //    result.Should().Be(true);
        //}

        //[Fact]
        //public void ResetPassword_InvalidResetPassword_Fail()
        //{
        //    //Act
        //    var result = userGateway.ResetPassword("User6", "Password123!@");
        //    //Assert
        //    result.Should().Be(true);
        //}

        ////It will fail since it will not find EditUserName8Twice, because it has already changed...

        ////[Fact]
        ////public void EditUser_ValidEditUser_Pass()//does not like this...
        ////{
        ////    var user = new EditUserDto()
        ////    {
        ////        Username = "EditUserName8TWICE",
        ////        NewUsername = "EditUsername8_3",
        ////        NewDisplayName = "EditUserDisplayName8_3",
        ////        NewPassword = "EditUserPassword8_3"
        ////    };
            
        ////    var result = userGateway.EditUser(user);
        ////    result.Should().Be(true);//Errors out here
        ////}
        ////[Fact]
        ////public void EditUser_Invalid_Fail()
        ////{
        ////    var user = new EditUserDto()
        ////    {
        ////        Username = "User2",
        ////        NewUsername = "EditUser2",
        ////        NewDisplayName = "EditDisplayName2",
        ////        NewPassword = "EditNewUsername2"
        ////    };

        ////    var result = userGateway.EditUser(user);//Failed here...
        ////    result.Should().Be(true);
        ////}
        //[Fact]
        //public void EditUser_ValidNewDisplayName_Pass()
        //{
        //    //Act
        //    var user = new EditUserDto()
        //    {
        //        Username = "EditUserName8TWICE",
        //        NewUsername = "EditUsername8_3",
        //        NewDisplayName = "New_88_Display",
        //        NewPassword = "EditUserPassword8_3"
        //    };
        //    //Assert
        //    var result = userGateway.EditUser(user);
        //    result.Should().Be(true);
        //}
        //[Fact]
        //public void EditUser_ValidNewPassword_Pass()
        //{
        //    //Act
        //    var user = new EditUserDto()
        //    {
        //        Username = "EditUserName8TWICE",
        //        NewUsername = "EditUsername8_3",
        //        NewDisplayName = "EditUserDisplayName8_3",
        //        NewPassword = "New_88_Pass"
        //    };
        //    //Assert
        //    var result = userGateway.EditUser(user);
        //    result.Should().Be(true);
        //}
        //[Fact]
        //public void EditUser_ValidNewUsername_Pass()
        //{
        //    //Act
        //    var user = new EditUserDto()
        //    {
        //        Username = "New_88_Username",
        //        NewUsername = "EditUsername8_3",
        //        NewDisplayName = "EditUserDisplayName8_3",
        //        NewPassword = "EditUserPassword8_3"
        //    };
        //    //Assert
        //    var result = userGateway.EditUser(user);
        //    result.Should().Be(true);
        //}
        //@TODO @Angelica Delete regular user
        [Fact]
        public void DeleteUser_ValidDelete_Pass()
        {
            //Act

            //Assert
            var result = userGateway.DeleteUser("User10");
            result.Should().Be(true);
        }
        //@TODO @Angelica Delete restaurant user
    }
}
