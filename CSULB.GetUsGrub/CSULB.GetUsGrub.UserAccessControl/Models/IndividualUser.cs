using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Defines the claims pertaining to an individual user
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    public class IndividualUser : IClaims
    {
        public string Type => AccountTypes.Individual;

        public ICollection<Claim> Claims => new List<Claim>
        {
            // For Individual Profie Management
            new Claim(ActionConstant.READ + ResourceConstant.INDIVIDUAL, "True"),
            new Claim(ActionConstant.UPDATE + ResourceConstant.INDIVIDUAL, "True"),

            // For Food Preferences
            new Claim(ActionConstant.READ + ResourceConstant.PREFERENCES, "True"),
            new Claim(ActionConstant.UPDATE + ResourceConstant.PREFERENCES, "True"),

            // For Bill Splitter  
            new Claim(ActionConstant.ACCESS + ResourceConstant.DICTIONARY, "True"),
            new Claim(ActionConstant.ACCESS + ResourceConstant.MENU, "True"),

            // For Restaurant Selector
            new Claim(ActionConstant.READ + ResourceConstant.RESTAURANTSELECTION, "True")
        };

    }
}
