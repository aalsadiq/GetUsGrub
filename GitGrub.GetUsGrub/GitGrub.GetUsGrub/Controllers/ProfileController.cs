using System;
using System.Web.Http;
using System.Web.Http.Cors;
using GitGrub.GetUsGrub.Managers;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Controllers
{
    /// <summary>
    /// Controller responsible for profile requests
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    [EnableCorsAttribute("*", "*", "*")]
    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private readonly ProfileManager _profileManager;
       
        public ProfileController(ProfileManager profileManager)
        {
            _profileManager = profileManager;
        }

        // GET api/profile/getregularprofile
        [HttpGet]
        [Route("getregularprofile")]
        public IHttpActionResult GetRegularProfile([FromBody] string username)
        {
            try
            {
                RegularProfileDto regularProfile = _profileManager.GetRegularProfile(username);
                return Ok(regularProfile);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // GET api/profile/getrestaurantprofile
        [HttpGet]
        [Route("getregularprofile")]
        public IHttpActionResult GetRestaurantProfile([FromBody] string username)
        {
            try
            {
                RestaurantProfileDto restaurantProfile = _profileManager.GetRestaurantProfile(username);
                return Ok(restaurantProfile);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT api/profile/editregularprofile
        [HttpPut]
        [Route("editregularprofile")]
        public IHttpActionResult EditRegularProfile([FromBody] EditRegularProfileDto editRegularProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _profileManager.EditRegularProfile(editRegularProfileDto);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok("Profile has been successfully updated.");
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
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok("Profile has been successfully updated.");
        }
    }
}