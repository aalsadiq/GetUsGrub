using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitGrub.GetUsGrub.Models;
//added
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GitGrub.GetUsGrub.Controllers
{
    public class DeleteUserController : ApiController
    {
        [Authorize(Roles = "Administrators")]
        [Route("api/DeleteUser/")]
        public IHttpActionResult Post(int id)
        {
           // User user = db.Books.Find(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //db.User.Remove(user);
           // db.SaveChanges();

            return Ok();//return user...
        }
    }
}
