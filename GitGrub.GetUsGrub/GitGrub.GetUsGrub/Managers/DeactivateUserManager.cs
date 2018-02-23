using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitGrub.GetUsGrub.Managers
{
    public class DeactivateUserManager
    {
        //Restructured
        /// <summary>
        /// Angelica
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public UserDto Deactivate(UserDto User)//RegisterRestaurantUserDto
        {
            if( checkIfUserDoesNotExist(User.Username) == true)//user does exist
            {
                using (var gateway = new UserGateway())
                {
                    return gateway.Deactivate(User);
                }
            }
            return User; //return what was inputted.
          
        }

        /// <summary>
        /// Calls the gateway. It checks if the user exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
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

        //strategy

    }
}

////check if user exists
//var gatewayResult = gateway.GetUserByUsername(User.Username);//will return the whole user

//                if (gatewayResult.Username == User.Username)//checking gateway and the input
//                {
//                    if(gatewayResult.IsActive == true)
//                    {
//                        User.IsActive = false;
//                        //deactivate logic
//                       var newUserInformation = gateway.Deactivate(User);
//                       return newUserInformation;//return new information from gateway...
//                    }
//                }
//                return User;//return what was inputed.
 ////validate length again
 //           using (var gateway = new UserGateway())
 //           {
 //               //check if user exists
 //               var gatewayResult = gateway.GetUserByUsername(username);

 //               if (gatewayResult.Username == username)//checking gateway and the input
 //               {
 //                   if (gateway.Deactivate(username) == true)//wsuccess change
 //                   {
 //                       return true;
 //                   }
 //               }
 //               return false;
 //           }

//public RegisterUserDto DeactivateIndividual(string username)
//{
//    //validate length again

//    using (var gateway = new UserGateway())
//    {
//        //check if user exists, then deactivate user
//        var gatewayResult = gateway.GetUserByUsername(username);
//        if (gatewayResult.Username == username)
//        {
//            //call gateway to set the user to inactive aka false
//            //have gateway return IndividualUser
//        }

//        //return 

//    }
// }//