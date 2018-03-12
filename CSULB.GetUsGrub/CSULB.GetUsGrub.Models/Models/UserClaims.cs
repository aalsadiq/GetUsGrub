using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Add data annotations? [-Jenn]
    /// <summary>
    /// The <c>Claims</c> class.
    /// Defines properties pertaining to a user's claims.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserClaims
    {
        [Key]
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }
        public IList<Claim> Claims { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
    }
}
