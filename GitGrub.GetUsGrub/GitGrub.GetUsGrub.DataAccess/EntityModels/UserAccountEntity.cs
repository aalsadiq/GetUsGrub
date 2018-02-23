using GitGrub.GetUsGrub.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// A duplicate user account entity to abstract the ORM framework.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 2/21/18
    /// </summary>
    [Table("GetUsGrub.UserAccount")]
    public class UserAccountEntity : IUserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required username.")]
        public string Username { get; set; }

        // Stored as a hash
        [Required(ErrorMessage = "Required password.")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
