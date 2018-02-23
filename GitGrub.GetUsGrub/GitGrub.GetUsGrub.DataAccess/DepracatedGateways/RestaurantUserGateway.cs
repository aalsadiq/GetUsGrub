using GitGrub.GetUsGrub.Models;
using System;

namespace GitGrub.GetUsGrub.DataAccess
{
    public class RestaurantUserGateway : IRestaurantUserGateway, IDisposable
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

        public bool StoreUser(RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
