using System.Data.Entity;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context that allows connections to specific entities for authorizaton module features.
    /// 
    /// Brian Fann
    /// 2/21/18
    /// </summary>
    public class AuthorizationContext : DbContext
    {
        // TODO Add UserClaims entity once it is implemented

        public DbSet<UserAccountEntity> UserAccounts { get; set; }

        public AuthorizationContext() : base("GetUsGrub") { }
    }
}
