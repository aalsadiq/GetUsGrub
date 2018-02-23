using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitGrub.GetUsGrub.Managers
{
    public class DeleteUserManager
    {
        /// <summary>
        /// Angelica
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool Delete(UserDto User)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                //check if user exists
                var gatewayResult = gateway.GetUserByUsername(User.Username);//will return the whole user

                if (gatewayResult.Username == User.Username)//checking gateway and the input
                {
                    var newUserInformation = gateway.Delete(User.Username);
                    return newUserInformation;//return new information from gateway...
                }
                return false;//return what was inputed.
            }
        }

        //add to a different folder...
        public bool checkIfUserDoesNotExist(string username)
        {
            using (var gateway = new UserGateway())
            {
                //check if user exists
                var gatewayResult = gateway.GetUserByUsername(username);//will return the whole user

                if (gatewayResult.Username == username)//checking gateway and the input
                {
                    return true;
                }
                return false;//return what was inputed.
            }
        }
    }
}