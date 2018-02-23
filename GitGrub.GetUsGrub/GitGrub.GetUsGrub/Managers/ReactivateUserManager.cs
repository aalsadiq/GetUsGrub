using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitGrub.GetUsGrub.Managers
{
    public class ReactivateUserManager
    {
        /// <summary>
        /// Angelica
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public UserDto Reactivate(UserDto User)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                //check if user exists
                var gatewayResult = gateway.GetUserByUsername(User.Username);//will return the whole user

                if (gatewayResult.Username == User.Username)//checking gateway and the input
                {
                    if (gatewayResult.IsActive == false)
                    {
                        User.IsActive = true;
                        //deactivate logic
                        var newUserInformation = gateway.Deactivate(User);
                        return newUserInformation;//return new information from gateway...
                    }
                }
                return User;//return what was inputed.
            }
        }

        public bool checkIfUserDoesNotExist(string username)
        {
            return false;//user does not exist
        }
    }
}