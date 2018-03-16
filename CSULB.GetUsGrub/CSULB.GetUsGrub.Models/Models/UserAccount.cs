using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    [Table("GetUsGrub.UserAccount")]
    public class UserAccount : IUserAccount, IEntity
    {
        [Required(ErrorMessage = "Required username.")]
        public string Username { get; set; }

        // Stored as a hash
        [Required(ErrorMessage = "Required password.")]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool IsFirstTimeUser { get; set; }

        // Schema data
        [Key]
        public int Id { get; set; }

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }

        public virtual UserClaims UserClaims { get; set; }

        public virtual PasswordSalt PasswordSalt { get; set; }

        public virtual Token Token { get; set; }

        public virtual ICollection<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
