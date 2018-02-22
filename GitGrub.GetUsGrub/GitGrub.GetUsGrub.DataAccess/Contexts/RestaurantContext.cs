using System.Data.Entity;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context that allows connections to specific entities for restaurant selection and restaurant profile management.
    /// 
    /// Brian Fann
    /// 2/21/18
    /// </summary>
    public class RestaurantContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<RestaurantProfile> RestaurantProfiles { get; set; }
        public DbSet<RestaurantMenu> RestaurantMenus { get; set; }
        public DbSet<RestaurantMenuItem> RestaurantMenuItems { get; set; }

        public RestaurantContext() : base("GetUsGrub") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
