using System.Data.Entity;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context for athorizaton module features
    /// 
    /// Brian Fann
    /// 2/21/18
    /// </summary>
    public class AuthorizationContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        // TO DO: Add UserClaims once it is implemented
    }
}
