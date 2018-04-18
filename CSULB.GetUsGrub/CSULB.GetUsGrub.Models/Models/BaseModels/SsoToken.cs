using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SsoToken</c> class.
    /// Contains properties pertaining to a SSO Token.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class SsoToken : IEntity
    {
        // Automatic Properties
        [Key]
        public int? Id { get; set; }
        public string Token { get; set; }
        [NotMapped]
        public SsoTokenPayloadDto SsoTokenPayloadDto { get; set; }
    }
}
