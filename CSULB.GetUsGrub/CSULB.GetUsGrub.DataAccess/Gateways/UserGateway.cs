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
    public class UserGateway
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

    //    public void Dispose()
    //    {
    //        //throw new NotImplementedException();
    //    }
    //    /// <summary>
    //    /// Will deactivate user by username by changing IsActive to false.
    //    /// </summary>
    //    /// <param name="username"></param>
    //    /// <returns></returns>
    //    public bool DeactivateUser(string username)
    //    {
    //        using (var dbContextTransaction = context.Database.BegingTransaction())
    //        {
    //            try
    //            {
    //                context.Database.ExecuteSqlCommand(

    //                //TODO: edit isactive to false
    //                );
    //                return true;

    //            }
    //            catch (Exception ex)
    //            {
    //                dbContextTransaction.Rollback();
    //            }
    //        }
    //        return true;
    //    }
    //    //added by me...
    //    public bool Reactivate(string username)
    //    {
    //        //TODO: IsActive
    //        return true;
    //    }
    //    public bool DeleteUser(string username)
    //    {
    //        return true;
    //    }
    //    public RegisterUserDto EditUser(RegisterUserDto user)
    //    {
    //        return user;
    //    }
    //    public RegisterRestaurantUserDto EditRestaurant(RegisterRestaurantUserDto user)
    //    {
    //        return user;
    //    }
    //}
}
