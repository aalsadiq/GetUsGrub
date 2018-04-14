using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.IdentityModel.Tokens;

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
        // Automatic properties
        [Key]
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }
        public string Username { get; set; }
        public DateTime ExpiresOn { get; set; }
        public SymmetricSecurityKey Key { get; set; }
        public string TokenString { get; set; }

        // Navigation Properties
        public virtual UserAccount UserAccount { get; set; }

        // Constructors
        public AuthenticationToken() { }

        public AuthenticationToken(string username,  DateTime expiresOn, string tokenString)
        {
            Username = username;
            ExpiresOn = expiresOn;
            TokenString = tokenString;
        }

        public AuthenticationToken(int? id, string username, DateTime expiresOn, string tokenString)
        {
            Id = id;
            Username = username;
            ExpiresOn = expiresOn;
            TokenString = tokenString;
        }
    }
}
