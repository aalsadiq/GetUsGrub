using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using System;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// UserProfileGatewayUnitTest
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class UserProfileGatewayUnitTests
    {
        [Fact]
        public void EditProfileWithValidInput()
        {
            // Arrange
            string username = "User1";

            var userProfile = new UserProfile()
            {
                DisplayName = "GetGrubIsTheBest1234",
                DisplayPicture = "SomePictureString"
            };

            var profileGateway = new UserProfileGateway();
            // Act
            Action act = () => profileGateway.EditUserProfileByUserProfileDomain(username, userProfile);

            // Assert
            act.Should().NotThrow();
        }
    }
}
