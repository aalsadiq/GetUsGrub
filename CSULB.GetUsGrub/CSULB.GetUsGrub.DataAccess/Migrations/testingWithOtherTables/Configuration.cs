namespace CSULB.GetUsGrub.DataAccess.Migrations.testingWithOtherTables
{
    using CSULB.GetUsGrub.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CSULB.GetUsGrub.DataAccess.AuthenticationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\testingWithOtherTables";
        }

        protected override void Seed(CSULB.GetUsGrub.DataAccess.AuthenticationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var userWithSalt = new List<PasswordSalt>
            {
                new PasswordSalt(){ UserId = 1, Salt ="salt1" },
                new PasswordSalt(){ UserId = 2, Salt ="salt1" },
                new PasswordSalt(){ UserId = 3, Salt ="salt1" },
                new PasswordSalt(){ UserId = 4, Salt ="salt1" },
                new PasswordSalt(){ UserId = 5, Salt ="salt1" },
                new PasswordSalt(){ UserId = 6, Salt ="salt1" },
                new PasswordSalt(){ UserId = 7, Salt ="salt1" },
                new PasswordSalt(){ UserId = 8, Salt ="salt1" },
                new PasswordSalt(){ UserId = 9, Salt ="salt1" },
                new PasswordSalt(){ UserId = 10, Salt ="salt1" }
            };

            context.PasswordSalts.AddOrUpdate(x => x.UserId, (userWithSalt.ToArray()));
            context.SaveChanges();

        }
    }
}
