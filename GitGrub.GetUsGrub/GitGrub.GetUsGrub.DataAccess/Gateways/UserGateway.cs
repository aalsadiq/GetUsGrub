using GitGrub.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.DataAccess
{
    public class UserGateway : IDisposable
    {

        public IUserAccount GetUserByUsername(string username)
        {
            var user = new UserAccount()
            {
                Username = "null",
                DisplayName = "null",
                Password = "null",
                IsActive = false
            };

            return user;
        }

        public bool StoreUserAccount(IUserAccount userAccount)
        {
            return true;
        }

        public bool StorePasswordSalt(string username, string salt)
        {
            return true;
        }

        public bool StoreSecurityQuestion(string username, ISecurityQuestion securityQuestion)
        {
            return true;
        }

        public bool StoreSecurityAnswerSalt(string username, string questionType, string salt)
        {
            return true;
        }

        public bool StoreClaims(string username, ICollection<Claim> claims)
        {
            return true;
        }

        public bool StoreRestaurantAccount(string username, IRestaurantAccount restaurantAccount)
        {
            return true;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
