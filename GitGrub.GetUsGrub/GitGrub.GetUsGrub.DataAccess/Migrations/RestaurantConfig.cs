namespace GitGrub.GetUsGrub.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class RestaurantConfig : DbMigrationsConfiguration<GitGrub.GetUsGrub.DataAccess.RestaurantContext>
    {
        public RestaurantConfig()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GitGrub.GetUsGrub.DataAccess.RestaurantContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
