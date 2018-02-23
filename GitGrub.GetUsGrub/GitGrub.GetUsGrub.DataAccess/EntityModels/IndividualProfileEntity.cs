using GitGrub.GetUsGrub.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// A duplicate individual profile entity to abstract the ORM framework.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 2/21/18
    /// </summary>
    [Table("GetUsGrub.IndividualProfile")]
    public class IndividualProfileEntity : IProfile
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }

        // NAVIGATION
        public UserAccountEntity UserAccount { get; set; }
    }
}