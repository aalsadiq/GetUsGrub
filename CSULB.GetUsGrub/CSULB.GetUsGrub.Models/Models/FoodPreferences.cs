using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// TODO: @Rachel Should FoodPreferences be changed to FoodPreference since it is only holding one preference? [-Jenn]
namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Defines properties pertaining to user's food preferences
    /// Note to self: talk to Brian confirming how this is stored, and check if set up properly done
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    [Table("GetUsGrub.FoodPreferences")]
    public class FoodPreferences : IPreference, IEntity
    {
        // Automatic Properties
        [Key]
        public int? Id { get; set; }

        [ForeignKey("UserAccount")]
        public int? UserId { get; set; }

        [Required]
        public string Preference { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }

        // Constructors
        public FoodPreferences() { }

        public FoodPreferences(string preference)
        {
            Preference = preference;
        }
    }
}
