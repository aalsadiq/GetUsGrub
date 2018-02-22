using System.Data.Entity;
using GitGrub.GetUsGrub.Models;s

namespace GitGrub.GetUsGrub.DataAccess
{
    public class AuthorizationContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set;
        }
    }
}
