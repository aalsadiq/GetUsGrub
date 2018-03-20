using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Need to create DbContext and comment this [-Jenn]
    [Table("GetUsGrub.SsoToken")]
    public class SsoToken : IEntity
    {
        [Key]
        public int? Id { get; set; }
        public string Token { get; set; }

        public SsoToken() {}

        public SsoToken(string token)
        {
            Token = token;
        }
    }
}
