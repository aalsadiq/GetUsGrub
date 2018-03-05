using System.Collections.Generic;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    public interface IClaims
    {
        ICollection<Claim> Claims { get; set; }
    }
}
