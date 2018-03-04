using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    [Validator(typeof(PasswordSaltValidator))]
    //TODO: Do we still need to declare a table?
    [Table("PasswordSalt")]
    public class PasswordSalt : ISalt
    {
        [Key]
        public int Id { get; set; }

        //TODO: Do we still need to declare Key and ForeignKey?
        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        public string Salt { get; set; }
    }
}
