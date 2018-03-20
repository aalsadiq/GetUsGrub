using CSULB.GetUsGrub.Models;
using System.Data.Entity;

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
        public DbSet<UserClaims> Claims { get; set; }
        public DbSet<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }
        public DbSet<BusinessHour> BussinessHours { get; set; }
        public UserContext() : base("GetUsGrub") { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserContext>()
        //        .HasOptional(a => a.)
        //        .WithOptionalDependent()
        //        .WillCascadeOnDelete(true);
        //}
    }

}
