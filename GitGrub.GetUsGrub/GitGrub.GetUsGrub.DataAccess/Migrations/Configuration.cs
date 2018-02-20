namespace GitGrub.GetUsGrub.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using GitGrub.GetUsGrub.Models;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GitGrub.GetUsGrub.DataAccess.AuthenticationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GitGrub.GetUsGrub.DataAccess.AuthenticationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.UserAccounts.AddOrUpdate(x => x.Id,
                new UserAccount() { Id = 0, Username = "TestUser1", Password = "a", AccountType = "B", IsActive = false },
                new UserAccount() { Id = 1, Username = "TestUser2", Password = "a", AccountType = "B", IsActive = false },
                new UserAccount() { Id = 2, Username = "TestUser2", Password = "a", AccountType = "B", IsActive = false }
            );

            context.SecurityQuestions.AddOrUpdate(x => x.Id,
                new SecurityQuestion() { Id = 0, UserId = 0, QuestionType = "a", QuestionAnswer = "a" },
                new SecurityQuestion() { Id = 1, UserId = 1, QuestionType = "a", QuestionAnswer = "a" },
                new SecurityQuestion() { Id = 2, UserId = 2, QuestionType = "a", QuestionAnswer = "a" }
            );
        }
    }
}
