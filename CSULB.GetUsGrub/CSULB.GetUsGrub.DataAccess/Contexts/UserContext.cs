using System.Data.Entity;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context containing all tables for user management.
    /// 
    /// @Created by: Brian Fann
    /// @Last Updated: 3/9/18
    /// </summary>

    public class UserContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<PasswordSalt> PasswordSalts { get; set; }

        public DbSet<RestaurantProfile> RestaurantProfiles { get; set; }

        public DbSet<RestaurantMenu> RestaurantMenus { get; set; }

        public DbSet<RestaurantMenuItem> RestaurantMenuItems { get; set; }

        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public UserContext() : base("GetUsGrub") { }
    }

}
