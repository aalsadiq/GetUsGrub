using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserAccount</c> class.
    /// Defines the information related to the AuthenticationToken
    /// <para>
    /// @author: Ahmed Alsadiq
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class AuthenticationToken : IEntity
    {
        [Key]
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }

        public string Username { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string Salt { get; set; }
        public string TokenString { get; set; }

        // Ask about this
        public AuthenticationToken()
        {
            this.Username = Username;
            this.ExpiresOn = ExpiresOn;
            this.Salt = Salt;
            this.TokenString = TokenString;
        }

        public AuthenticationToken(AuthenticationTokenDto authenticationTokenDto)
        {
            Username = authenticationTokenDto.Username;
            ExpiresOn = authenticationTokenDto.ExpiresOn;
            TokenString = authenticationTokenDto.TokenString;
        }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
        
    }
}
