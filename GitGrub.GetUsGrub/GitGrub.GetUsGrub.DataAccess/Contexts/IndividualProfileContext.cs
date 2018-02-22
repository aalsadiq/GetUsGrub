using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.DataAccess
{
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
            public DbSet<UserAccount> UserAccounts { get; set; }
            public DbSet<IndividualProfile> SecurityQuestions { get; set; }
            public DbSet<Token> Tokens { get; set; }
            public DbSet<PasswordSalt> PasswordSalts { get; set; }
            public DbSet<Token> TokenSalts { get; set; }

            public IndividualProfileContext() : base("GetUsGrub") { }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
