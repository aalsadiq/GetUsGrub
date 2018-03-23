using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserProfile</c> class.
    /// Defines properties pertaining to a user's profile.
    /// <para>
    /// @author: Andrew Kao, Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.UserProfile")]
    public class UserProfile : IProfile, IEntity
    {
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }
        public string DisplayPicture { get; set; }
        public string DisplayName { get; set; }
        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }
        public virtual RestaurantProfile RestaurantProfile { get; set; }
    }
}