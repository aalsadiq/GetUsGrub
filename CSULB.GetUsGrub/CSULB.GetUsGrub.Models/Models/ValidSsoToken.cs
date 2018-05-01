using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>ValidSsoToken</c> class.
    /// Defines properties pertaining to a valid SSO Token.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.ValidSsoToken")]
    public class ValidSsoToken : SsoToken
    {
        // Constructors
        public ValidSsoToken() { }

        public ValidSsoToken(string token)
        {
            Token = token;
        }
    }
}
