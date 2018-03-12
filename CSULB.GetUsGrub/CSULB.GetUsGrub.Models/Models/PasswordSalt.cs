using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    [Table("GetUsGrub.PasswordSalt")]
    public class PasswordSalt : ISalt, IEntity
    {
        [ForeignKey("UserAccount")]
        public int Id { get; set; }

        public string Salt { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
    }
}
