namespace CSULB.GetUsGrub.DataAccess.Migrations.SsoDbContext
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CSULB.GetUsGrub.DataAccess.SsoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\SsoDbContext";
        }

        protected override void Seed(CSULB.GetUsGrub.DataAccess.SsoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
