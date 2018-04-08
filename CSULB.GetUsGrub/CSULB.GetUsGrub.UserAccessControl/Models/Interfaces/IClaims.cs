using System.Collections.Generic;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Defines the interface for a claims collection object
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    public interface IClaims
    {
        string Type { get; }
        ICollection<Claim> Claims { get; }
    }
}
