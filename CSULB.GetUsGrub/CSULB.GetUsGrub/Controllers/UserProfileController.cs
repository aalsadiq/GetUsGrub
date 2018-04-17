using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.Drawing;//For Images
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
        [AllowAnonymous] // TODO: remember to change localhosts to 8080
        [Route("User")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        public IHttpActionResult GetProfile(string username)
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

                return Ok(response.Data); //TODO: make sure to have responses as response.Data
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
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "PUT")]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Update")]
        [HttpPut]
        public IHttpActionResult ProfileImageUpload()//UserProfileDto //HttpPostedFileBase HttpRequestMessage
        {
            try
            {
                var file = HttpContext.Current.Request.Files[0];
                var filePath = HttpContext.Current.Server.MapPath(@"~/Images/DisplayProfileImages/" + file.FileName);
                file.SaveAs(filePath);
                return Created("Created", $"Created {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest("This is a bad request.");
            }
        }

        //// TODO: @Angelica ImageUpload comments
        //// PUT Profile/User/EditUser/ImageUpload
        //[Route("User/Get/ProfileImage")]
        //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "PUT")]
        ////[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Update")]
        //[HttpGet]
        //public IHttpActionResult GetProfileImage() //IHttpActionResult
        //{
        //    try
        //    {
        //        // Validate that is is an image (extensions)




        //        // var filePath = HttpContext.Current.Server.MapPath(@"~/Images/DisplayProfileImages/DefaultProfileImage.png");
        //        //var filePath = @"~/Images/DisplayProfileImages/DefaultProfileImage.png";
        //        //using (FileStream fs = new FileStream(filePath, FileMode.Open))
        //        //{
        //        //    IHttpActionResult response = new IHttpActionResult();
             
        //        //    response.ContentType = new MediaTypeHeaderValue("image/jpeg");
        //        //    return response;
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //        //If any exceptions occur, send an HTTP response 400 status.
        //        return BadRequest("This is a bad request.");
        //    }
        //}


    }
}
