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
    public class AuthenticationContext : DbContext
    {
        public virtual DbSet<UserAccountEntity> UserAccounts { get; set; }
        public virtual DbSet<SecurityQuestionEntity> SecurityQuestions { get; set; }
        public virtual DbSet<TokenEntity> Tokens { get; set; }
        public virtual DbSet<PasswordSaltEntity> PasswordSalts { get; set; }
        public virtual DbSet<TokenSaltEntity> TokenSalts { get; set; }

        public AuthenticationContext() : base("GetUsGrub") { }
    }
}
