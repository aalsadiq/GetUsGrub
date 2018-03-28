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

    public class AuthenticationContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<PasswordSalt> PasswordSalts { get; set; }
        public DbSet<AuthenticationToken> AuthenticationTokens { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<SsoToken> SsoTokens { get; set; }
        public DbSet<FailedAttempts> FailedAttempts { get; set; }

        public AuthenticationContext() : base("GetUsGrub") { }
    }

}
