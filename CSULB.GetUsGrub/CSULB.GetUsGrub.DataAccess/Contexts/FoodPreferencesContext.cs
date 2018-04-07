using CSULB.GetUsGrub.Models;
using System.Data.Entity;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context containing all tables for Food Preferences
    /// @author: Rachel Dang
    /// @updated: 04/06/18
    /// </summary>
    class FoodPreferencesContext : DbContext
    {
        public DbSet<FoodPreferences> FoodPreferences { get; set; }

        public FoodPreferencesContext() : base("GetUsGrub") { }
    }
}
