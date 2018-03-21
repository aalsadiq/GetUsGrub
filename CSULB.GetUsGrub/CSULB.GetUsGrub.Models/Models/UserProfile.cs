using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// User profile domain model
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    
    [Table("GetUsGrub.UserProfiles")]
    public class UserProfile : IUserProfile
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.UserAccounts")]
        public int? UserId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public string DisplayPicture { get; set; }

        //TODO: @andrew @rachel add food preferences

        public UserProfile(string name, string picture)
        {
            DisplayName = name;
            DisplayPicture = picture;
        }

        public UserProfile()
        {
            DisplayName = null;
            DisplayPicture = null;
        }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
        public virtual RestaurantProfile RestaurantProfile { get; set; }
    }
}
