using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment this  [-Jenn]
    public class SsoToken : IEntity
    {
        // Automatic Properties
        [Key]
        public int? Id { get; set; }
        public string Token { get; set; }
        [NotMapped]
        public SsoTokenPayload SsoTokenPayload { get; set; }
    }
}
