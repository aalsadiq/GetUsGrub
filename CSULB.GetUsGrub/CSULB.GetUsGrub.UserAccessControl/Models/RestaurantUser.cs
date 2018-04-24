using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Defines the claims pertaining to a restaurant user
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    public class RestaurantUser : IClaims
    {
        public string Type => AccountType.Restaurant;

        public ICollection<Claim> Claims => new List<Claim>
        {
            // For Restaurant Profie Management
            new Claim(ActionConstant.READ+ResourceConstant.RESTAURANT, "True"),
            new Claim(ActionConstant.UPDATE+ResourceConstant.RESTAURANT, "True"),

            // For Bill Splitter
            new Claim(ActionConstant.ACCESS+ResourceConstant.MENU, "True")
        };
    }
}
