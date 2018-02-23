using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.DataAccess;
using System;

namespace GitGrub.GetUsGrub.Tests
{
    [TestClass]
    public class GatewayIntegrationTests
    {
        [TestClass]
        public class TestAuthenticationGateway
        {
            [TestInitialize]
            public void InitTest()
            {
            }
            [TestMethod]
            public void TestGetUserByUsername()
            {
                var username = "JohnDoe";
                var expected = "abc";

                using (IAuthenticationGateway gateway = new AuthenticationEntityGateway())
                {
                    var actual = gateway.GetUserByUsername(username);
                    Assert.AreEqual(expected, actual?.Password);
                }
            }

            [TestMethod]
            public void TestGetSaltByUserId()
            {
                var userId = 0;
                var expected = "abc";

                using (IAuthenticationGateway gateway = new AuthenticationEntityGateway())
                {
                    var actual = gateway.GetPasswordSaltByUserId(userId);
                    Assert.AreEqual(expected, actual.Salt);
                }
            }

            [TestMethod]
            public void TestAddToken()
            {
                var token = new Token()
                {
                    TokenHeader = "abc",
                    TokenSignature = "abc",
                    ExpiresOn = DateTime.Now,
                    IssuedOn = DateTime.Today,
                };

                using(IAuthenticationGateway gateway = new AuthenticationEntityGateway())
                {
                    gateway.AddToken((IToken)token);
                }
            }
        }
    }
}
