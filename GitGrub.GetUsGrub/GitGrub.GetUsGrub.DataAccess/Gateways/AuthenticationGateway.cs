using System;
using System.Collections.Generic;
using System.Linq;

namespace GitGrub.GetUsGrub.DataAccess
{
    public class AuthenticationGateway : IAuthenticationGateway
    {
        // Get user by username
        // Get users by username
        // Create user
        // Get password by username
    }
}
/*
 *  public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required username.")]
        public string Username { get; set; }

        // Stored as a hash
        [Required(ErrorMessage = "Required password.")]
        public string Password { get; set; }

        public string AccountType { get; set; }

        public bool IsActive { get; set; }
    }

    [Table("SecurityQuestion")]
    public class SecurityQuestion
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }

        [Required(ErrorMessage = "Required security question")]
        public string QuestionType { get; set; }

        [Required(ErrorMessage = "Required security question answer")]
        public string QuestionAnswer { get; set; }
    }
}
 */
