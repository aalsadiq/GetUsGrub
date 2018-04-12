using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserClaims</c> class.
    /// Defines properties pertaining to a user's claims.
    /// <para>
    /// @author: Brian Fann
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.UserClaims")]
    public class UserClaims : IEntity
    {
        // Automatic Properties
        [Key]
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }
        public string ClaimsJson
        {
            get => JsonConvert.SerializeObject(Claims);
            set => Claims = JsonConvert.DeserializeObject<List<Claim>>(value, new ClaimConverter());
        }
        [NotMapped]
        public ICollection<Claim> Claims { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }

        // Constructors
        public UserClaims() { }

        public UserClaims(ICollection<Claim> claims)
        {
            Claims = claims;
        }
    }
}