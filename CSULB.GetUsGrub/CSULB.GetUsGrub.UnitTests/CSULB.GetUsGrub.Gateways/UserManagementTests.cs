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
        public UserGateway userGateway = new UserGateway();

        [Fact]
        public void DeactivateUser_When_GivenUserNameUser1()
        {
            var result =  userGateway.DeactivateUser("User10");
            Assert.True(result);
        }

        [Fact]
        public void Should_DeactivateUser_When_Given_UserName_User2()
        {
            var result = userGateway.DeactivateUser("User9");
            Assert.True(result);
        }

        [Fact]
        public void Should_ReactivateUser_When_Given_UserName_User1()
        {
            var result = userGateway.ReactivateUser("User10");
            Assert.True(result);
        }
        //[Fact]
        //public void EditUserName_ValidEditUserName_Pass()//Fail...-It's okay thought! (should only have one fail)
        //{
        //    var result = userGateway.EditUserName("User1", "NewUser1");//because there is no user1 anymore... it's NewUser1
        //    Assert.True(result);
        //}
        [Fact]
        public void EditUserName_InvalidEditUserName_Fail()
        {
            var result = userGateway.EditUserName("User2", "User2");
            result.Should().Be(true);
        }
        [Fact]
        public void EditDisplayName_ValidEditDisplayName_Pass()
        {
            var result = userGateway.EditDisplayName("User3", "NewDisplayName3");
            result.Should().Be(true);
        }
        [Fact]
        public void EditDisplayName_InvalidEditDisplayName_Fail()
        {
            var result = userGateway.EditDisplayName("User4", "DisplayName4");
            result.Should().Be(true);
        }

        [Fact]
        public void ResetPassword_ValidResetPassword_Pass()
        {
            var result = userGateway.ResetPassword("User4", "NewPassword4!");
            result.Should().Be(true);
        }

        [Fact]
        public void ResetPassword_InvalidResetPassword_Fail()
        {
            var result = userGateway.ResetPassword("User6", "Password123!@");
            result.Should().Be(true);
        }

        //It will fail since it will not find EditUserName8Twice, because it has already changed...

        //[Fact]
        //public void EditUser_ValidEditUser_Pass()//does not like this...
        //{
        //    var user = new EditUserDto()
        //    {
        //        Username = "EditUserName8TWICE",
        //        NewUsername = "EditUsername8_3",
        //        NewDisplayName = "EditUserDisplayName8_3",
        //        NewPassword = "EditUserPassword8_3"
        //    };
            
        //    var result = userGateway.EditUser(user);
        //    result.Should().Be(true);//Errors out here
        //}
        [Fact]
        public void EditUser_Invalid_Fail()
        {
            var user = new EditUserDto()
            {
                Username = "EditUserName8_3",
                NewUsername = "EditUsername8_3",
                NewDisplayName = "EditUserDisplayName8_3",
                NewPassword = "EditUserPassword8_3"
            };

            var result = userGateway.EditUser(user);
            result.Should().Be(true);
        }
        [Fact]
        public void EditUser_ValidNewUserName_Pass()
        {

        }
        [Fact]
        public void EditUser_ValidNewDisplayName_Pass()
        {

        }
        [Fact]
        public void EditUser_ValidNewPassword_Pass()
        {

        }
    }
}
