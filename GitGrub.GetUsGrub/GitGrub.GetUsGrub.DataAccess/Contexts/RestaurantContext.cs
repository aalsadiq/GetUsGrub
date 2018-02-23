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
        public DbSet<UserAccountEntity> UserAccounts { get; set; }
        public DbSet<RestaurantProfileEntity> RestaurantProfiles { get; set; }
        public DbSet<RestaurantMenuEntity> RestaurantMenus { get; set; }
        public DbSet<RestaurantMenuItemEntity> RestaurantMenuItems { get; set; }

        public RestaurantContext() : base("GetUsGrub") { }
    }
}
