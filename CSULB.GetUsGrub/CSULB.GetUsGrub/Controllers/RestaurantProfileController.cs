using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.Web;
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
    [RoutePrefix("Profile")]
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
        [Route("Restaurant/Edit/MenuItemImageUpload")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        // TODO: @Angelica Check what claims are needed here [Angelica!]
        [HttpPost]
        public IHttpActionResult MenuItemImageUpload()
        {
            try
            {

                var image = HttpContext.Current.Request.Files[0];
                var username = HttpContext.Current.Request.Params["username"];
                var stringMenuId = HttpContext.Current.Request.Params["menuId"];

                var menuId = Convert.ToInt32(stringMenuId);
                Debug.WriteLine("menuItem: " + menuId);

                if (username == null || username == "")
                {
                    return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
                }

                var manager = new RestaurantProfileManager();
                var response = manager.MenuItemImageUpload(image, username, menuId);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok("Image Upload complete!");
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
    }
}