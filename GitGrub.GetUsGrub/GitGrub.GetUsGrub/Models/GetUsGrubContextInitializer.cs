using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;//add this so you can drop and create database always
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Models
{
    public class GetUsGrubContextInitializer:DropCreateDatabaseAlways<GetUsGrubContext>
    {
        protected override void Seed(GetUsGrubContext context)
        {
            var users = new List<User>
            {
                new User() {}
            };

            // context.Users.Add(users);
            //users.ForEach(o => context.Users.Add(o));
            //context.SaveChanges();
            //users.ForEach(b => context.users.Add(b));//adding books to database
            //context.SaveChanges(); //to save the changes
            //foreach (User usertest in users)
            //    context.Users.Add(usertest);
                base.Seed(context);
        }

    }
}