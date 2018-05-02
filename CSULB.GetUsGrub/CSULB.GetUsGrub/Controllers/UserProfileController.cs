using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.IdentityModel.Services;
using System.Security.Permissions;
using System.Web;
using System.Web.Http;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// User profile controller
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    [RoutePrefix("api/v1/Profile")]
    public class UserProfileController : ApiController
    {
        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.INDIVIDUAL, Operation = ActionConstant.READ)]
        [Route("User")]
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

            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.INDIVIDUAL, Operation = ActionConstant.UPDATE)]
        [Route("User/Edit")]
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

            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // PUT Profile/User/EditUser/ImageUpload
        [Route("User/Edit/ProfileImageUpload")]
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

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
