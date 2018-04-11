using CSULB.GetUsGrub.Models;
using System.Data.Entity;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context containing all tables for user management.
    /// 
    /// @Created by: Brian Fann
    /// @Last Updated: 4/10/18
    /// </summary>
    public class IndividualProfileContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<FoodPreference> FoodPreferences { get; set; }
        public IndividualProfileContext() : base("GetUsGrub") { }
    }
}
