using System;
using System.Collections.Generic;
using System.Data.Entity;
using GitGrub.GetUsGrub.Models.Models;


namespace GitGrub.GetUsGrub.DataAccess.Context
{
    // This represents a session between the data base and the Server
    public class EntityContext : DbContext
    {
        internal readonly object RegisterUser;

        public EntityContext() : base("DefaultConnection")
        {
        }
        // These are representation of the Models in the Models Folder 
        public DbSet<UserAccount> UserModel { get; set; }
        public DbSet<TokenModel> TokenModel { get; set; }
        public DbSet<PasswordSalt> PasswordSalts { get; set; }

    }
}

