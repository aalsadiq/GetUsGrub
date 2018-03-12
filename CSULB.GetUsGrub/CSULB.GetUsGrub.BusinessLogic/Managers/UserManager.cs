using CSULB.GetUsGrub.DataAccess.Gateways;
using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic.Managers
{
    /// <summary>
    /// Manager that will handel CRUD.
    /// @authors Jen, Angelica
    /// Last Updated: 03-10-2018
    /// </summary>
    public class UserManager
    {
        //TODO: @Jen Add Create User here.

        /// <summary>
        /// Will deactivate user when given username.
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// </summary>C:\Users\Angelica\Documents\GetUsGrub\CSULB.GetUsGrub\CSULB.GetUsGrub.BusinessLogic\Managers\UserManager.cs
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeactivateUser(string username)
        {
            //Validate DTO - in this case validate if it follows business rule names
            //Map to Domain Models - In this case I don't believe I need to model bind
            //Apply Business Logic - validating username rules?
            //Validate Domain Model - Domain model has not changed.

            using (var gateway = new UserGateway())
            {
                return gateway.DeactivateUser(username);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ReactivateUser(string username)
        {
            using (var gateway = new UserGateway())
            {
                return gateway.ReactivateUser(username);
            }
        }

        /// <summary>
        /// Manager that will handle business logic for delete user along with calling UserGateway.
        /// </summary>
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeleteUser(string username)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.DeleteUser(username);
            }
        }

        /// <summary>
        /// Will take in a Registered User to modify
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RegisterUserDto EditUser(RegisterUserDto user)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.EditUser(user);//will return edited user
            }
        }

        /// <summary>
        /// Will take in a Registered restaurant user to modify.
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public RegisterRestaurantDto EditRestaurant(RegisterRestaurantDto user)//RegisterRestaurantUserDto
        {
            using (var gateway = new UserGateway())
            {
                return gateway.EditRestaurant(user);//will return edited user...
            }
        }
    }
}
