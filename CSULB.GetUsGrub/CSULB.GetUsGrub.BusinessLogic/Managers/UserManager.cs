using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic.Managers
{
    public class UserManager
    {
        //TODO: @Jen Add Create User

        /// <summary>
        /// Will deactivate user when given username.
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeactivateUser(string username)
        {
            //using (var gateway = new UserGateway())
            //{
            //    return gateway.DeactivateUser(username);
            //}

            return true; //Place holder
        }


        public bool DeleteUser(string username)//RegisterRestaurantUserDto
        {
            //using (var gateway = new UserGateway())
            //{
            //    return gateway.DeleteUser(username);
            //}

            return true;//place holder
         }

        /// <summary>
        /// Will take in a Registered User to modify
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public RegisterUserDto EditUser(RegisterUserDto user)//RegisterRestaurantUserDto
        //{
        //    using (var gateway = new UserGateway())
        //    {
        //        return gateway.EditUser(user);
        //    }
        //}

        /// <summary>
        /// Will take in a Registered restaurant user to modify.
        /// @author Angelica
        /// @Last Update: 03/10/2018
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public RegisterRestaurantUserDto EditRestaurant(RegisterRestaurantUserDto user)//RegisterRestaurantUserDto
        //{
        //    using (var gateway = new UserGateway())
        //    {
        //        return gateway.EditRestaurant(user);
        //    }
        //}
    }
}
