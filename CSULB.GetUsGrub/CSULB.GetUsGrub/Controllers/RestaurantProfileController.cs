using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.IdentityModel.Services;
using System.Security.Permissions;
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
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.RESTAURANT, Operation = ActionConstant.READ)]
        [Route("Restaurant")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        public IHttpActionResult GetProfile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var profileManager = new RestaurantProfileManager();
                var response = profileManager.GetProfile(Request.Headers.Authorization.Parameter);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                return Ok(response.Data);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.RESTAURANT, Operation = ActionConstant.UPDATE)]
        [Route("Restaurant/Edit")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        public IHttpActionResult EditProfile([FromBody] RestaurantProfileDto restaurantProfileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var profileManager = new RestaurantProfileManager();
                var response = profileManager.EditProfile(restaurantProfileDto, Request.Headers.Authorization.Parameter);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                return Ok(response.Data);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // TODO: @Angelica ImageUpload comments
        // PUT Profile/User/Edit/MenuItemImageUpload
        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.IMAGE, Operation = ActionConstant.UPDATE)]
        [Route("Restaurant/Edit/MenuItemImageUpload")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
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