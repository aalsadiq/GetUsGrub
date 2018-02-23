using GitGrub.GetUsGrub.Models;

using System.Data.Entity;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context that allows connections to specific entities for authentication module features.
    /// 
    /// Brian Fann
    /// 2/21/18
    /// </summary>
    public class IndividualProfileContext : DbContext
    {
        // TODO Add food preference entity once it is implemented

        public DbSet<UserAccountEntity> UserAccounts { get; set; }
        public DbSet<IndividualProfileEntity> IndividualProfiles { get; set; }

        public IndividualProfileContext() : base("GetUsGrub") { }
    }
}