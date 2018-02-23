using System;
using System.Web.Http;
using System.Web.Http.Cors;
using GitGrub.GetUsGrub.Managers;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Controllers
{
    /// <summary>
    /// Controller responsible for restaurant profile requests
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    [EnableCorsAttribute("*", "*", "*")]
    [Authorize]
    [RoutePrefix("api/restaurantprofile")]
    public class RestaurantProfileController : ApiController
    {
        private readonly RestaurantProfileManager _profileManager;

        public RestaurantProfileController(RestaurantProfileManager profileManager)
        {
            _profileManager = profileManager;
        }

        // GET api/profile/getrestaurantprofile
        [HttpGet]
        [Route("getrestaurantprofile")]
        public IHttpActionResult GetRestaurantProfile([FromBody] string username)
        {
            try
            {
                ResponseDto<RestaurantProfileDto> response = _profileManager.GetRestaurantProfile(username);
                return Ok(response);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT api/profile/editrestaurantprofile
        [HttpPut]
        [Route("editrestaurantprofile")]
        public IHttpActionResult EditRestaurantProfile([FromBody] EditRestaurantProfileDto editRestaurantProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _profileManager.EditRestaurantProfile(editRestaurantProfileDto);
                return Ok("Profile has been successfully updated.");
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}