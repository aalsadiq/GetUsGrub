using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Defines properties pertaining to user's food preferences
    /// Note to self: talk to Brian confirming how this is stored, and check if set up properly done
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/18
    /// </summary>
    [Table("GetUsGrub.FoodPreference")]
    public class FoodPreference : IPreference, IEntity
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
        public FoodPreference() { }

        public FoodPreference(string preference)
        {
            Preference = preference;
        }
    }
}
