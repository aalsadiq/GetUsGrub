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
    public class CreateUserController : ApiController
    {
       // public UserContext db = new UserContext();
        //creating
        [Authorize(Roles = "Administrators")]
        [Route("api/admin/createuser")]
        [HttpPost]
        public IHttpActionResult CreateUser(Object user)
        {
           
            //if (user.Equals("Admin"))//just a place holder
            //{
            //    //save to db
              return Ok();
            //}
            //else
            //{
            //    return BadRequest();
            //}
           //return RedirectToRoute("CreateUserBusinessLogic");
        }

        [Authorize(Roles = "Administrators")]
        [Route("api/admin/viewuser/{id}")]
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            //var user.
           // var user = new User { };

            if(id == 0)
            {
                return NotFound();
            }
            else
            {
                //get from database
                return Ok();//return user..
            }
            
        }

        //public IQueryable<User> GetAllUsers()
        //{
        //    //return db.Users;
        //    return User;
        //}
        //get/api/values

    }
}
