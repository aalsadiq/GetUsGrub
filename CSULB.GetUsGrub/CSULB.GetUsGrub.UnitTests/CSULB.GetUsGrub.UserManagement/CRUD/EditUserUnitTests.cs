using CSULB.GetUsGrub.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests.CSULB.GetUsGrub.UserManagement.CRUD
{
    public class EditUserUnitTests
    {
        public UserGateway userGateway = new UserGateway();

        [Fact]
        public void Should_DeactivateUser_When_GivenUserNameUser1()
        {
            var result =  userGateway.DeactivateUser("User1");
            Assert.True(result);
        }

        [Fact]
        public void Should_DeactivateUser_When_Given_UserName_User2()
        {
            var result = userGateway.DeactivateUser("User2");
            Assert.True(result);
        }

        [Fact]
        public void Should_ReactivateUser_When_Given_UserName_User1()
        {
            var result = userGateway.ReactivateUser("User1");
            Assert.True(result);
        }

        //    [Fact]
        //    public void Should_DeleteUser_When_Given_UserName_User5()
        //    {
        //        var result = userGateway.DeleteUser("User5");
        //        Assert.True(result);
        //    }

        [Fact]
        public void Should_Edit_Username_When_Given_EditDto_User1()
        {
            //var result = userGateway.EditUserName("User1");
            //Assert.True(result);
        }
    }
}
