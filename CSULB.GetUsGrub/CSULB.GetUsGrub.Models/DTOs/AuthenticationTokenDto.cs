using System;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserAccount</c> class.
    /// Defines the fields needed from the Token to be Transfered.
    /// <para>
    /// @author: Ahmed Alsadiq
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class AuthenticationTokenDto
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string TokenString { get; set; }

        public AuthenticationTokenDto(string username, DateTime expiresOn, string tokenString)
        {
            Username = username;
            ExpiresOn = expiresOn;
            TokenString = tokenString;     
        }
    }
}
