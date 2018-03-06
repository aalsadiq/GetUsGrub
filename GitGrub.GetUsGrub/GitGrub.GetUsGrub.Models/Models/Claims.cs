using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>Claims</c> class.
    /// Defines properties pertaining to a user's claims.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class Claims : IClaims
    {
        // TODO: Is this how to serialize list into JSON?
        [JsonProperty(PropertyName = "Claims")]
        public ICollection<Claim> ClaimsList { get; set; }
    }
}
