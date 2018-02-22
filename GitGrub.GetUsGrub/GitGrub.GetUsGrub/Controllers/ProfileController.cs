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
                ResponseDto<RegularProfileDto> response = _profileManager.GetRegularProfile(username);
                return Ok(response);
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
               ResponseDto<RestaurantProfileDto> response = _profileManager.GetRestaurantProfile(username);
               return Ok(response);
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
                ResponseDto<bool> response = _profileManager.EditRegularProfile(editRegularProfileDto);
                return Ok("Profile has been successfully updated.");
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