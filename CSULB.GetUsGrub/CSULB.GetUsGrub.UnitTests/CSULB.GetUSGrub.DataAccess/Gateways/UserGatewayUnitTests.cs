using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// The <c>UserGatewayUnitTests</c> class.
    /// Contains unit tests for the UserGateway.
    /// <para>
    /// @author: Jennifer Nguyen, Rachel Dang
    /// @updated: 04/11/2018
    /// </para>
    /// </summary>
    public class UserGatewayUnitTests
    {
        [Fact]
        public void Should_StoreUser_When_ValidModelsArePassedIn()
        {
            // Arrange
            var userAccount = new UserAccount()
            {
                Username = "username",
                Password = "!Q2w#E4r",
                IsActive = true,
                IsFirstTimeUser = false
            };
            var passwordSalt = new PasswordSalt()
            {
                Salt = "!@#$%^&*()"
            };
            var claims = new UserClaims()
            {
                Claims = new Collection<Claim>()
                {
                    new Claim("ReadIndividualProfile", "True"),
                    new Claim("UpdateIndividualProfile", "True"),
                    new Claim("ReadPreferences", "True"),
                    new Claim("UpdatePreferences", "True"),
                    new Claim("ReadBillSplitter", "True"),
                    new Claim("ReadMenu", "True"),
                    new Claim("ReadDictionary", "True"),
                    new Claim("UpdateDictionary", "True")
                }
            };
            var securityQuestions = new List<SecurityQuestion>()
            {
                new SecurityQuestion()
                {
                    Question = 1,
                    Answer = "answer1",
                },
                new SecurityQuestion()
                {
                    Question = 2,
                    Answer = "answer2",
                },
                new SecurityQuestion()
                {
                    Question = 3,
                    Answer = "answer3",
                }
            };
            var securityAnswerSalts = new List<SecurityAnswerSalt>()
            {
                new SecurityAnswerSalt()
                {
                    Salt = "!Q2w#E4r"
                },
                new SecurityAnswerSalt()
                {
                    Salt = "%T6y&U8i"
                },
                new SecurityAnswerSalt()
                {
                    Salt = "&U8i(O0p"
                }
            };
            var userProfile = new UserProfile()
            {
                DisplayPicture = "www.img.com",
                DisplayName = "displayName"
            };
            var userGateway = new UserGateway();

            // Act
            Action act = () => userGateway.StoreIndividualUser(userAccount, passwordSalt, securityQuestions, securityAnswerSalts, claims, userProfile);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Should_ReturnPreferences_When_UsernameIsPassedIn()
        {
            // Arrange
            string username = "username1";
            UserGateway gateway = new UserGateway();

            // Act
            var result = gateway.GetFoodPreferencesByUsername(username);

            // Assert
            result.Data.Should().NotBeNull();
            result.Error.Should().BeNull();
        }
        
        [Fact]
        public void Should_ReturnError_When_InvalidUsernameIsPassedIn()
        {
            // Arrange
            string username = "InvalidUser";
            UserGateway gateway = new UserGateway();

            // Act
            var result = gateway.GetFoodPreferencesByUsername(username);

            // Assert
            result.Data.Should().BeNull();
            result.Error.Should().NotBeNull();
        }

        [Fact]
        public void Should_ReturnTrue_When_FoodPreferencesAreUpdated()
        {
            // Arrange
            string username = "overlord39";
            var updatedFoodPreferences = new List<string>
            {
                "Lacto-Vegetarian",
                "Pescetarian",
                "Vegan"
            };

            UserGateway gateway = new UserGateway();

            // Act
            var result = gateway.EditFoodPreferencesByUsername(username, updatedFoodPreferences);

            //result.Data.Should().BeTrue();
            result.Error.Should().BeNull();
        }
    }
}
