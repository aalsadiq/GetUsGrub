using System.Collections.Generic;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The IClaims interface.
    /// A contract with defined property for Claims class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public interface IClaims
    {
        ICollection<Claim> ClaimsList { get; set; }
    }
}
