using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace GitGrub.GetUsGrub.Models.Models
{
    class UserContext: DbContext
    {
        public UserContext() : base("name=UserContext")
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
