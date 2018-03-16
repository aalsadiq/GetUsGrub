using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>Claims</c> class.
    /// Defines properties pertaining to a user's claims.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.UserClaims")]
    public class UserClaims
    {
        [ForeignKey("UserAccount")]
        public int Id { get; set; }

        [NotMapped]
        public IList<Claim> Claims { get; set; }

        public string ClaimsJson
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(Claims);
            }
            set
            {
                Claims = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Claim>>(value);
            }
        }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
    }
}
