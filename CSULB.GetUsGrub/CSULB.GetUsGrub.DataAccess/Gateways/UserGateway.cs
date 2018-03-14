using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.DataAccess.Gateways
{
    /// <summary>
    /// @author: Jennifer Nguyen, Angelica Salas
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserGateway:IDisposable
    {
        //    public IUserAccount GetUserByUsername(string username)
        //    {
        //        var user = new UserAccount()
        //        {
        //            Username = "null",
        //            DisplayName = "null",
        //            Password = "null",
        //            IsActive = false
        //        };

        //        if (username == "userExists")
        //        {
        //            return user;
        //        }

        //        return null;
        //    }

        //    public bool StoreUserAccount(IUserAccount userAccount)
        //    {
        //        return true;
        //    }

        //    public bool StorePasswordSalt(string username, string salt)
        //    {
        //        return true;
        //    }

        //    public bool StoreSecurityQuestion(string username, ISecurityQuestion securityQuestion)
        //    {
        //        return true;
        //    }

        //    public bool StoreSecurityAnswerSalt(string username, int questionType, string salt)
        //    {
        //        return true;
        //    }

        //    public bool StoreClaims(string username, ICollection<Claim> claims)
        //    {
        //        return true;
        //    }

        //    public bool StoreRestaurantAccount(string username, IRestaurantAccount restaurantAccount)
        //    {
        //        return true;
        //    }

        //    public bool StoreUserProfile(string displayName)
        //    {
        //        return true;
        //    }

        //    public bool StoreRestaurantProfile(string username)
        //    {
        //        return true;
        //    }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Will deactivate user by username by changing IsActive to false.
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeactivateUser(string username)
        {
            //using (var dbContextTransaction = context.Database.BegingTransaction())
            //{
            //    try
            //    {
            //        context.Database.ExecuteSqlCommand(

            //        //TODO: edit isactive to false
            //        );
            //        return true;

            //    }
            //    catch (Exception ex)
            //    {
            //        return false;
            //        dbContextTransaction.Rollback();
            //    }
            //}
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ReactivateUser(string username)
        {
            //TODO: @Angelica Reactivate
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="username"></param>
        /// <returns></returns>
        public bool DeleteUser(string username)
        {
            //TODO: @Angelica Delete
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="user">The user that will be edited.</param>
        /// <returns></returns>
        public RegisterUserDto EditUser(RegisterUserDto user)
        {
            //TODO: @Angelica EditUsser
            return user;
        }

//User Related!
        public bool EditDisplayName()
        {
            return true;
        }
        public bool ResetPassword()//EditPassword
        {
            return true;
        }
//Restaurant Related! 
        public bool EditBusinessHours()
        {
            return true;
        }
        //public bool EditDisplayPicture()
        //{
        //    return true;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// @author Angelica Salas Tovar
        /// Last Updated: 03-10-2018
        /// <param name="user">The restaurant user that will be edited.</param>
        /// <returns></returns>
        public RegisterRestaurantDto EditRestaurant(RegisterRestaurantDto user)
        {
            //TOTOD: @Angelica EditRestaurant
            return user;
        }
    }
}
