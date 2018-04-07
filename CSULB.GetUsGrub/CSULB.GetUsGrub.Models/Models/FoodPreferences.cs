using System;
using System.Collections.Generic;
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
    [Table("GetUsGrub.FoodPreferences")]
    public class FoodPreferences : IPreferences, IEntity
    {
        // Automatic Properties
        [Key]
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }

        [Required]
        public ICollection<String> Preferences { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }

        // Constructors
        public FoodPreferences() { }

        public FoodPreferences(ICollection<string> preferences)
        {
            Preferences = preferences;
        }
    }
}
