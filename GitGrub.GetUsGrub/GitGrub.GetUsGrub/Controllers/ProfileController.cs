using System;
using System.Web.Http;
using System.Web.Http.Cors;
using GitGrub.GetUsGrub.Managers;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]

    public class ProfileController : ApiController
    {
        private readonly ProfileManager _profileManager;

        public ProfileController(ProfileManager profileManager)
        {
            _profileManager = profileManager;
        }

        // PUT api/<controller>
        [Route("api/profile/editregularprofile")]
        [HttpPut]
        public IHttpActionResult EditRegularProfile([FromBody] RegularProfile editRegularProfileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _profileManager.EditRegularProfile(editRegularProfileDTO);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(editRegularProfileDTO);
        }

        // PUT api/<controller>
        [Route("api/profile/editrestaurantprofile")]
        [HttpPut]
        public IHttpActionResult EditRestaurantProfile([FromBody] RestaurantProfile editRestaurantProfileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _profileManager.EditRestaurantProfile(editRestaurantProfileDTO);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(editRestaurantProfileDTO);
        }
    }
}