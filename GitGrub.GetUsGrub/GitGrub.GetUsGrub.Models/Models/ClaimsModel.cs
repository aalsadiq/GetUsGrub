using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    public class ClaimsModel : IClaims
    {
        // TODO: Is this how to serialize list into JSON?
        [JsonProperty(PropertyName = "Claims")]
        public ICollection<Claim> Claims { get; set; }
    }
}
