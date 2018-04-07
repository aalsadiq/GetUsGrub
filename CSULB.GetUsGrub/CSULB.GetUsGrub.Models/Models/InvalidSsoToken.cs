using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SsoToken</c> class.
    /// Defines properties pertaining to a valid Sso Token.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.InvalidSsoToken")]
    public class InvalidSsoToken : SsoToken
    {
        // Constructors
        public InvalidSsoToken() { }
        public InvalidSsoToken(string token)
        {
            Token = token;
        }
    }
}
