using GitGrub.GetUsGrub.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GitGrub.GetUsGrub.Models.Models
{
    [Table("UserAccount")]
    public class UserAccount : IUserAccount
    {
        [Key]
        public int Id { get; set; }

        [Key]
        [Required(ErrorMessage = "Required username")]
        public string UserName { get; set; }

        // How to differentiate from a no input password to a hashed password?
        // Stored as a hash
        [Required(ErrorMessage = "Required password")]
        public string Password { get; set; }

        // Will we need to know when the user was created?
        // public DateTime CreateOn { get; set; }

        public string AccountType { get; set; }

        //[Required(ErrorMessage = "Active status of user required")]
        public bool IsActive { get; set; }

        //[Required(ErrorMessage = "Required security questions and answers")]
        //public Collection<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
