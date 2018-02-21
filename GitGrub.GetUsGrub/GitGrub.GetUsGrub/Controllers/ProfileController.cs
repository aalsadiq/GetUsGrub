using System;
using System.Web.Http;
using System.Web.Http.Cors;
using GitGrub.GetUsGrub.Managers;
using GitGrub.GetUsGrub.Models;

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
        public IHttpActionResult GetRegularProfile([FromBody] RegularProfile getRegularProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _profileManager.GetRegularProfile(getRegularProfileDto);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(getRegularProfileDto);
        }

        // GET api/profile/getrestaurantprofile
        [HttpGet]
        [Route("getregularprofile")]
        public IHttpActionResult GetRestaurantProfile([FromBody] RestaurantProfile getRestaurantProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _profileManager.GetRestaurantProfile(getRestaurantProfileDto);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(getRestaurantProfileDto);
        }

        // PUT api/profile/editregularprofile
        [HttpPut]
        [Route("editregularprofile")]
        public IHttpActionResult EditRegularProfile([FromBody] RegularProfile editRegularProfileDto)
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

            return Ok(editRegularProfileDto);
        }

        // PUT api/profile/editrestaurantprofile
        [HttpPut]
        [Route("editrestaurantprofile")]
        public IHttpActionResult EditRestaurantProfile([FromBody] RestaurantProfile editRestaurantProfileDto)
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

            return Ok(editRestaurantProfileDto);
        }
    }
}