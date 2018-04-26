using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.IdentityModel.Services;
using System.Security.Permissions;

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
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.INDIVIDUAL, Operation = ActionConstant.READ)]
        [Route("User")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        public IHttpActionResult GetProfile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var profileManager = new UserProfileManager();
                var response = profileManager.GetProfile(Request.Headers.Authorization.Parameter);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                return Ok(response.Data); 
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.INDIVIDUAL, Operation = ActionConstant.UPDATE)]
        [Route("User/Edit")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]   
        public IHttpActionResult EditProfile([FromBody] UserProfileDto userProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var profileManager = new UserProfileManager();
                var response = profileManager.EditProfile(userProfileDto, Request.Headers.Authorization.Parameter);
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

        // PUT Profile/User/EditUser/ImageUpload
        [Route("User/Edit/ProfileImageUpload")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.IMAGE, Operation = ActionConstant.UPDATE)]
        [HttpPost]
        public IHttpActionResult ProfileImageUpload() 
        {
            try
            {
                var image = HttpContext.Current.Request.Files[0];
                var username = HttpContext.Current.Request.Params["username"];

                if (username == null || username == "")
                {
                    return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
                }

                var manager = new UserProfileManager();
                var response = manager.ProfileImageUpload(image, username);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok("Image Upload complete!");
            }

            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
    }
}
