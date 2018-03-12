using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// User profile domain model
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>
    
    [Table("GetUsGrub.UserProfile")]
    public class UserProfile : IUserProfile
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.UserAccount")]
        public int? UserId { get; set; }

        public string DisplayName { get; set; }

        public string DisplayPicture { get; set; }
    }
}
