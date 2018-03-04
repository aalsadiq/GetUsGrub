using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    // TODO: Should I include this table annotation?
    [Table("SecurityAnswerSalt")]
    public class SecurityAnswerSalt : ISalt
    {
        [Key]
        public int Id { get; set; }

        //TODO: Do we still need to declare Key and ForeignKey?
        [ForeignKey("SecurityQuestion")]
        public int SecurityQuestionId { get; set; }

        public string Salt { get; set; }
    }
}
