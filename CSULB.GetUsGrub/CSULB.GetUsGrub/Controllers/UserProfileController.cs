using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// User profile controller
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    [RoutePrefix("Profile")]
    public class UserProfileController : ApiController
    {
        [HttpGet]
        [AllowAnonymous] // TODO: Remove for deployment
        [Route("User")]
        [EnableCors(origins: "http://localhost:8081", headers: "*", methods: "*")]
        public IHttpActionResult GetProfile([FromBody] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var profileManager = new UserProfileManager();
                var response = profileManager.GetProfile(username);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                return Ok(response);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [AllowAnonymous] // TODO: Remove for deployment
        [Route("User/Edit")]
        [EnableCors(origins: "http://localhost:8081", headers: "*", methods: "*")]
        public IHttpActionResult EditProfile([FromBody] UserProfileDto userProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var profileManager = new UserProfileManager();
                var response = profileManager.EditProfile(userProfileDto);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                return Ok(response);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // TODO: @Angelica ImageUpload comments
        // PUT Profile/User/EditUser/ImageUpload
        [Route("User/Edit/ProfileImageUpload")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Update")]
        [HttpPut]
        public IHttpActionResult ProfileImageUpload([FromBody] HttpPostedFileBase user)//UserProfileDto
        {
            //Checks if what was given is a valid model.
            if (!ModelState.IsValid)
            {
                //If model is invalid, return a bad request.
                return BadRequest("Something went wrong in controller.");
            }
            try
            {
                //work in progress... (testing...)
                string directory = @"C:\Users\Angelica\Desktop\TEST\";
                if(user!= null && user.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(user.FileName);
                    user.SaveAs(Path.Combine(directory, fileName));
                }
                
                //var filePath = user.DisplayPicture;
                //Console.WriteLine("This is the image path" + filePath);
                //Creating a manager to then call ProfileImageUpload.
                //var manager = new UserProfileManager();
                //Calling ProfileImageUpload method to edit the given user.
                //var response = manager.ProfileImageUpload(user);
                //Checks the response from ProfileImageUpload. If error is null, then it was successful.
                //if (response.Error != null)
                //{
                //    //Will return a bad request if error occured in manager.
                //    return BadRequest(response.Error);
                //}
                //return Ok("Image has been updated");
                return Ok(user);
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest("This is a bad request.");
            }
        }
    }
}
