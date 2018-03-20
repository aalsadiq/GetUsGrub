using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
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

        [HttpGet]
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
    }
}
