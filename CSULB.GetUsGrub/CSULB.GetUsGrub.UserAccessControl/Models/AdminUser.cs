using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Defines the claims pertaining the admin users
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    public class AdminUser : IClaims
    {
        public string Type => AccountTypes.Admin;

        public ICollection<Claim> Claims => new List<Claim>
        {
            // For User Management
            new Claim(ActionConstant.CREATE + ResourceConstant.USER, "True"),
            new Claim(ActionConstant.READ + ResourceConstant.USER, "True"),
            new Claim(ActionConstant.UPDATE + ResourceConstant.USER, "True"),
            new Claim(ActionConstant.DELETE + ResourceConstant.USER, "True"),
            new Claim(ActionConstant.DEACTIVATE + ResourceConstant.USER, "True"),
            new Claim(ActionConstant.REACTIVATE + ResourceConstant.USER, "True"),

            // For Indvidiual and Restaurant Profile Management
            new Claim(ActionConstant.READ + ResourceConstant.INDIVIDUAL, "True"),
            new Claim(ActionConstant.UPDATE + ResourceConstant.INDIVIDUAL, "True"),
            new Claim(ActionConstant.READ + ResourceConstant.RESTAURANT, "True"),
            new Claim(ActionConstant.UPDATE + ResourceConstant.RESTAURANT, "True"),

            // For Food Preferences
            new Claim(ActionConstant.CREATE + ResourceConstant.PREFERENCES, "True"),
            new Claim(ActionConstant.READ + ResourceConstant.PREFERENCES, "True"),
            new Claim(ActionConstant.UPDATE + ResourceConstant.PREFERENCES, "True"),
            new Claim(ActionConstant.DELETE + ResourceConstant.PREFERENCES, "True"),

            // For Bill Splitter
            new Claim(ActionConstant.ACCESS + ResourceConstant.DICTIONARY, "True"),
            new Claim(ActionConstant.ACCESS + ResourceConstant.MENU, "True")
        };
}
}
