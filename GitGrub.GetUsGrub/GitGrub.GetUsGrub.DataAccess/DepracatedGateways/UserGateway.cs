using GitGrub.GetUsGrub.Models;
using System;

namespace GitGrub.GetUsGrub.DataAccess
{
    public class UserGateway : IUserGateway, IDisposable
    {

        public IUserAccount GetUserByUsername(string username)
        {
            var user = new UserAccount()
            {
                Username = "null",
                Password = "null",
                IsActive = false
            };

            return user;
        }

        public bool StoreUser(RegisterUserDto registerUserDto)
        {
            return true;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
