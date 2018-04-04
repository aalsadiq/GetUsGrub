using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SsoToken</c> class.
    /// Defines properties pertaining to an Sso Token.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.SsoToken")]
    public class SsoToken : IEntity
    {
        // Automatic Properties
        [Key]
        public int? Id { get; set; }
        public string Token { get; set; }

        // Constructors
        public SsoToken() {}

        public SsoToken(string token)
        {
            Token = token;
        }
    }
}
