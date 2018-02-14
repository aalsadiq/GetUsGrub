using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitGrub.GetUsGrub.Models.Models;
//added
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GitGrub.GetUsGrub.Models;
namespace GitGrub.GetUsGrub.Controllers
{
    public class CreateUserController : ApiController
    {
        //Creating the service that will handle the business logic (UserManager.cs)
        public UserManager UserManager { get; }
        //create a controller for the service...
        public CreateUserController(UserManager userManager)
        {
            UserManager = userManager;
        }

        //[Authorize(Roles = "Administrators")]//restricting by role --claim = isadmin
        [Route("api/CreateUser/")]
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody] User user)
        {//from body...

            var user2 = new User();

            if(user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, user);
            }
            else
            {
                //try
                //{
                //    // UserManager
                //    return Ok();
                //}
                //catch ()
                //{

                //}


                return Request.CreateResponse(HttpStatusCode.OK, user);
            }


        }





        //return new HttpResponseMessage()
        //{
        //    Content = new StringContent("Invalid User")
        //};


        //return new HttpResponseMessage()
        //{
        //    //Content = new StringContent("Post :Text message" )
        //};
        [Route("api/CreateUser/")]
        [HttpGet]
        public IHttpActionResult returnuser(Object user)
        {
            //sample
            //call context in there
            //var repo = new UserContext( new context ());
            //var results = repo.GetAllUsers()
            //    .orderby(f => f.Description)
            //    .take(10);
            //    .ToList();

            //return results;
            //return Ok(user);

            // var user = DbContext.User.Get();
            //if(user == null)
            //{
            //    //return Request.CreateResponse(HttpStatusCode.NotFound);
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, user);
            //}

            return Ok();

        }


        ////[Authorize(Roles = "Administrators")]//restricting by role
        //[Route("api/CreateUser/")]
        //[HttpGet]
        //public HttpResponseMessage GetUser(Object user)
        //{
        //    return new HttpResponseMessage()
        //    {
        //        Content = new StringContent("GET :Text message")
        //    };

        //}
















    }
}


        //public IEnumerable<User> GetAllUsers()
        //{
        //    return users;
        //}











        //// public UserContext db = new UserContext();
        ////creating
        //[Authorize(Roles = "Administrators")]//restricting by role
        //[Route("api/admin/createuser")]
        //[HttpPost]
        //public IHttpActionResult CreateUser(Object user)
        //{
           
        //    //if (user.Equals("Admin"))//just a place holder
        //    //{
        //    //    //save to db
        //      return Ok();
        //    //}
        //    //else
        //    //{
        //    //    return BadRequest();
        //    //}
        //   //return RedirectToRoute("CreateUserBusinessLogic");
        //}

        //[Authorize(Roles = "Administrators")]
        //[Route("api/admin/viewuser/{id}")]
        //[HttpGet]
        //public IHttpActionResult GetUser(int id)
        //{
        //    //var user.
        //    // var user = new User { };
        //  //  var user = DbContext.User.Get(id);

        //    if(id == 0)
        //    {
        //        return NotFound();
        //        //return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }
        //    else
        //    {
        //        //get from database
        //        return Ok();//return user..
        //    }
            
        //}

        ////public IQueryable<User> GetAllUsers()
        ////{
        ////    //return db.Users;
        ////    return User;
        ////}
        ////get/api/values
