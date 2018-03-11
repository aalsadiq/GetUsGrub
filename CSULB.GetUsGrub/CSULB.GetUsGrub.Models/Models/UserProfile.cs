using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Regular user profile class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    [Table("GetUsGrub.UserProfile")]
    public class UserProfile : IProfile, IEntity
    {
        [ForeignKey("UserAccount")]
        public int Id { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }

        public virtual RestaurantProfile RestaurantProfile { get; set; }
    }
}