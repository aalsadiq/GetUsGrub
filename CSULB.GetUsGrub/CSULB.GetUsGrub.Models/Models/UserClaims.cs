using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserClaims</c> class.
    /// Defines properties pertaining to a user's claims.
    /// <para>
    /// @author: Brian Fann, Jennifer Nguyen
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.UserClaims")]
    public class UserClaims : IEntity
    {
        [System.Serializable]
        internal class ClaimsEntry
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }

        public UserClaims() { }

        public UserClaims(ICollection<Claim> claims)
        {
            Claims = claims;
        }

        [Key]
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }

        [NotMapped]
        public ICollection<Claim> Claims { get; set; }

        [NotMapped]
        ICollection<ClaimsEntry> Entries
        {
            get
            {
                var entries = new Collection<ClaimsEntry>();

                // Exit early
                if (Claims == null) return entries;

                foreach (var claim in Claims)
                {
                    entries.Add(new ClaimsEntry { Type = claim.Type, Value = claim.Value });
                }

                return entries;
            }
            set
            {
                var claims = new Collection<Claim>();

                foreach (var entry in value)
                {
                    claims.Add(new Claim(entry.Type, entry.Value));
                }

                Claims = claims;
            }
        }

        public string ClaimsJson
        {
            get => JsonConvert.SerializeObject(Entries);
            set => Entries = JsonConvert.DeserializeObject<Collection<ClaimsEntry>>(value);
        }

        internal class ClaimsEntry
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
    }
}
