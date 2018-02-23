using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.Models.DTOs;
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
                DisplayName = "null",
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
///////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Angelica 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        //Place holder 
        public UserDto Deactivate(UserDto username)
        {
            //Deactivate Logic
            var placeholder = new UserDto();
            return placeholder;
        }

        /// <summary>
        /// Angelica 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        //Place holder
        public UserDto Reactivate(UserDto username)
        {
            //Reactivate Logic
            var placeholder = new UserDto();
            return placeholder;
        }
        /// <summary>
        /// Angelica 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        //Place holder
        public bool Delete(string username)
        {
            //Delete Logic Place holder
            return true;
        }
    }
}
