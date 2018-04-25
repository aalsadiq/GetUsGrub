using System.Collections.Generic;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Factory that creates a set of claims for new users
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 4/17/18
    /// </summary>
    public class ClaimsFactory : IFactory<ICollection<Claim>>
    {
        public ICollection<Claim> Create(string type)
        {
            switch(type)
            {
                // Create claims collection for an individual user
                case AccountType.Individual:
                    return new IndividualUser().Claims;

                // Create claims collection for a restaurant user
                case AccountType.Restaurant:
                    return new RestaurantUser().Claims;

                // Create claims collection for an admin user
                case AccountType.Admin:
                    return new AdminUser().Claims;

                // Create claims collection for first time user
                case AccountType.FirstTimeUser:
                    return new FirstTimeUser().Claims;
                
                // Return an empty list of claims for default
                default:
                    return new List<Claim> { };
            }
        }
    }
}
