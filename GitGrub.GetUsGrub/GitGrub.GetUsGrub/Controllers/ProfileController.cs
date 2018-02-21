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

    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private readonly ProfileManager _profileManager;
       
        public ProfileController(ProfileManager profileManager)
        {
            _profileManager = profileManager;
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