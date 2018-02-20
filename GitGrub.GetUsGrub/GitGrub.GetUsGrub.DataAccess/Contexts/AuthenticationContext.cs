using GitGrub.GetUsGrub.Models;
using System.Data.Entity;

namespace GitGrub.GetUsGrub.DataAccess
{
    public class AuthenticationContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }

        public AuthenticationContext():base("GetUsGrub") {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
