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

    public class RestaurantContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<RestaurantProfile> RestaurantProfiles { get; set; }

        public DbSet<RestaurantMenu> RestaurantMenus { get; set; }

        public DbSet<RestaurantMenuItem> RestaurantMenuItems { get; set; }

        public RestaurantContext() : base("GetUsGrub") { }
    }

}