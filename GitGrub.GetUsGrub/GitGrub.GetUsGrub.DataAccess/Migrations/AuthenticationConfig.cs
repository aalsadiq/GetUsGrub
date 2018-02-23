namespace GitGrub.GetUsGrub.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class AuthenticationConfig : DbMigrationsConfiguration<GitGrub.GetUsGrub.DataAccess.AuthenticationContext>
    {
        public AuthenticationConfig()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GitGrub.GetUsGrub.DataAccess.AuthenticationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
