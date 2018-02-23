using System;
using System.Web.Http;
using System.Web.Http.Cors;
using GitGrub.GetUsGrub.Managers;
using GitGrub.GetUsGrub.Models;

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
    public class IndividualProfileController : ApiController
    {
        private readonly IndividualProfileManager _profileManager;
       
        public IndividualProfileController(IndividualProfileManager profileManager)
        {
            _profileManager = profileManager;
        }

        // GET api/profile/getindividualprofile
        [HttpGet]
        [Route("getindividualprofile")]
        public IHttpActionResult GetIndividualProfile([FromBody] string username)
        {
            try
            {
                ResponseDto<IndividualProfileDto> response = _profileManager.GetIndividualProfile(username);
                return Ok(response);
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT api/profile/editindividualprofile
        [HttpPut]
        [Route("editindividualprofile")]
        public IHttpActionResult EditIndividualProfile([FromBody] EditIndividualProfileDto editIndividualProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResponseDto<bool> response = _profileManager.EditIndividualProfile(editIndividualProfileDto);
                return Ok("Profile has been successfully updated.");
            }

            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}