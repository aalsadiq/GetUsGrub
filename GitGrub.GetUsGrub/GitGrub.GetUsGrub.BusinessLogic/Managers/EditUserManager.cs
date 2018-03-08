using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitGrub.GetUsGrub.BusinessLogic.Managers
{
    /// <summary>
    /// Manager that will handle editing users
    /// </summary>
    public class EditUserManager
    {
        /// <summary>
        /// Will take in a Registered User to modify
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RegisterUserDto EditUser(RegisterUserDto user)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.EditUser(user);
            }
        }

        /// <summary>
        /// Will take in a Registered restaurant user to modify.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RegisterRestaurantUserDto EditRestaurant(RegisterRestaurantUserDto user)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.EditRestaurant(user);
            }
        }
    }
}
