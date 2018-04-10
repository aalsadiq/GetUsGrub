using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// Restaurant profile controller
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary> 
    [RoutePrefix("Profile/User")]
    public class RestaurantProfileController : ApiController
    {
        [HttpGet]
        [AllowAnonymous] // TODO: Remove for deployment
        [Route("Restaurant")]
        [EnableCors(origins: "http://localhost:8081", headers: "*", methods: "*")]
        public IHttpActionResult GetProfile([FromBody] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var profileManager = new RestaurantProfileManager();
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

        [HttpPut]
        [AllowAnonymous] // TODO: Remove for deployment
        [Route("Restaurant/Edit")]
        [EnableCors(origins: "http://localhost:8081", headers: "*", methods: "*")]
        public IHttpActionResult EditProfile([FromBody] RestaurantProfileDto restaurantProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var profileManager = new RestaurantProfileManager();
                var response = profileManager.EditProfile(restaurantProfileDto);
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


        // TODO: @Angelica ImageUpload comments
        // PUT Profile/User/Edit/MenuItemImageUpload
        [Route("Edit/MenuItemImageUpload")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        // TODO: @Angelica Check what claims are needed here [Angelica!]
        [HttpPut]
        public IHttpActionResult MenuItemImageUpload([FromBody] RestaurantProfileDto user)
        {
            //Checks if what was given is a valid model.
            if (!ModelState.IsValid)
            {
                //If model is invalid, return a bad request.
                return BadRequest("Something went wrong, please try again later");
            }
            try
            {
                //Creating a manager to then call EditUser.
                //var manager = new RestaurantProfileDto();
                //Calling EditUser method to edit the given user.
                //var response = manager.ProfileImageUpload(user.DisplayPicture);
                //Checks the response from EditUser. If error is null, then it was successful.
                //if (response.Error != null)
                //{
                //    //Will return a bad request if error occured in manager.
                //    return BadRequest(response.Error);
                //}
                //If EditUser was successful return HTTP response with a successful message.
                return Ok("Image has been updated.");
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest("This is a bad request.");
            }
        }
    }
}