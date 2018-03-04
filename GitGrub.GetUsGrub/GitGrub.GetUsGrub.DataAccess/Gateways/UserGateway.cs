using GitGrub.GetUsGrub.Models;
using System;

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

        public void Dispose()
        {
            //throw new NotImplementedException(); 
        }
    }
}