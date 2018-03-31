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
    [Table("GetUsGrub.AuthenticationToken")]
    public class AuthenticationToken : IEntity
    {
        [Key]
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }
        public string Username { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string Salt { get; set; }
        public string TokenString { get; set; }

        // TODO: @Ahmed Please delete this [-Jenn]
        public AuthenticationToken()
        {
            this.Username = Username;
            this.ExpiresOn = ExpiresOn;
            this.Salt = Salt;
            this.TokenString = TokenString;
        }

        // TODO: @Ahmed Please separate the DTO input into separate parameters next time. I created a new constructor to reflect this for you. Please delete this constructor below. [-Jenn]
        public AuthenticationToken(AuthenticationTokenDto authenticationTokenDto)
        {
            Username = authenticationTokenDto.Username;
            ExpiresOn = authenticationTokenDto.ExpiresOn;
            TokenString = authenticationTokenDto.TokenString;
        }

        public AuthenticationToken(int? id, string username, DateTime expiresOn, string salt, string tokenString)
        {
            Id = id;
            Username = username;
            ExpiresOn = expiresOn;
            Salt = salt;
            TokenString = tokenString;
        }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
    }
}
