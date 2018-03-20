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
        public UserProfile() { }

        public UserProfile(UserProfileDto userProfileDto)
        {
            DisplayPicture = userProfileDto.DisplayPicture;
            DisplayName = userProfileDto.DisplayName;
        }

        [ForeignKey("UserAccount")]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.UserAccounts")]
        public int? UserId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public string DisplayPicture { get; set; }
        public string DisplayName { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
        public virtual RestaurantProfile RestaurantProfile { get; set; }
    }
}
