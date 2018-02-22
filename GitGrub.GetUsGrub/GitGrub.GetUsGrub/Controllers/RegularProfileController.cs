using System;
using System.Web.Http;
using System.Web.Http.Cors;
using GitGrub.GetUsGrub.Managers;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Controllers
{
    /// <summary>
    /// Controller responsible for regualr profile requests
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    [EnableCorsAttribute("*", "*", "*")]
    [Authorize]
    [RoutePrefix("api/profile")]
    public class RegularProfileController : ApiController
    {
        private readonly RegularProfileManager _profileManager;
       
        public RegularProfileController(RegularProfileManager profileManager)
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
    }
}