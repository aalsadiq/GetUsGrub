namespace CSULB.GetUsGrub.DataAccess.Migrations.testUsermanagement
{
    using CSULB.GetUsGrub.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CSULB.GetUsGrub.DataAccess.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\testUsermanagement";
        }

        protected override void Seed(CSULB.GetUsGrub.DataAccess.UserContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var users = new List<UserAccount>()
            {
               new UserAccount() { Username = "User1", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User2", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User3", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User4", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User5", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User6", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User7", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User8", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User9", Password = "password123!@", IsActive = true, FirstTimeUser = true},
               new UserAccount() { Username = "User10", Password = "password123!@", IsActive = true, FirstTimeUser = true},
            };


            //users.ForEach(newUser => context.UserAccounts.AddOrUpdate(newUser)); //Adding users to database
            //context.UserAccounts.AddOrUpdate(x => x.Username, users);
            context.UserAccounts.AddOrUpdate(x => x.Username, (users.ToArray()));
            context.SaveChanges();//Save the changes

            //enable - migrations - ContextTypeName CSULB.GetUsGrub.DataAccess.UserContext - MigrationsDirectory:Migrations\testUsermanagement
            //add - migration - configuration CSULB.GetUsGrub.DataAccess.Migrations.testUsermanagement.Configuration
            //update - database - configuration CSULB.GetUsGrub.DataAccess.Migrations.testUsermanagement.Configuration - verbose
        }
    }
}
