using System.Collections.Generic;
using System.Security.Claims;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Defines the claims for first time users
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/17/18
    /// </summary>
    public class FirstTimeUser : IClaims
    {
        public string Type => AccountType.FirstTimeUser;

        public ICollection<Claim> Claims => new List<Claim>
        {
            new Claim(ActionConstant.READ + ResourceConstant.FIRSTTIMEUSER, "True")
        };
    }
}
