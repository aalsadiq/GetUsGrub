using System.Collections.Generic;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Factory that creates a set of claims for new users
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 4/07/18
    /// </summary>
    public class ClaimsFactory : IFactory<ICollection<Claim>>
    {
        public ICollection<Claim> Create(string type)
        {
            switch (type)
            {
                // Create claims collection for an individual user
                case AccountType.INDIVIDUAL:
                    return new IndividualClaims().Claims;

                // Create claims collection for a restaurant user
                case AccountType.RESTAURANT:
                    return new RestaurantClaims().Claims;

                // Create claims collection for an admin user
                case AccountType.ADMIN:
                    return new AdminClaims().Claims;
                
                // Return an empty list of claims for default
                default:
                    return new List<Claim> { };
            }
        }
    }
}
