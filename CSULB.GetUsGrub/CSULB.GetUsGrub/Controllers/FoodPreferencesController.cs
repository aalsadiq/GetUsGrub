using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.IdentityModel.Services;
using System.Security.Permissions;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// Controller to routes pertaining to Food Preferences
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/24/18
    /// </summary>
    [RoutePrefix("FoodPreferences")]
    public class FoodPreferencesController : ApiController
    {
        /// <summary>
        /// Routes to the method to get user's list of food preferences
        /// </summary>
        /// <returns>HTTP response with or without user's list of food preferences</returns>
        [HttpGet]
        [Route("GetPreferences")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "GET")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.PREFERENCES, Operation = ActionConstant.READ)]
        public IHttpActionResult GetPreferences()
        {
            // Check if model is valid for the database
            if (!ModelState.IsValid)
            {
                // If model is not valid, return bad request
                return BadRequest(ModelState);
            }
            try
            {
                // Get token string from request header
                var tokenString = Request.Headers.Authorization.Parameter;

                // If model is valid, call manager to get preferences
                var manager = new FoodPreferencesManager();
                var preferences = manager.GetFoodPreferences(tokenString);

                // If there is an error in the response, return a bad request
                if (preferences.Error != null)
                {
                    return BadRequest(preferences.Error);
                }

                // Otherwise, return the preferences
                return Ok(preferences.Data);
            }
            catch (Exception)
            {
                // If any other error occurs, return a bad request
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }

        /// <summary>
        /// Routes to the method to edit user's list of current food preferencees
        /// </summary>
        /// <param name="foodPreferencesDto"></param>
        /// <returns>HTTP response determining whether transaction was successful</returns>
        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.PREFERENCES, Operation = ActionConstant.UPDATE)]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        [Route("Edit")]
        public IHttpActionResult EditPreferences([FromBody] FoodPreferencesDto foodPreferencesDto)
        {
            // Check if model is valid for the database
            if (!ModelState.IsValid)
            {
                // If model is not valid, return bad request
                return BadRequest(ModelState);
            }
            try
            {
                // Get token string from request header
                var tokenString = Request.Headers.Authorization.Parameter;

                // If model is valid, call manager to get preferences
                var manager = new FoodPreferencesManager();
                var response = manager.EditFoodPreferences(tokenString, foodPreferencesDto);

                // If there is an error in the response, return a bad request
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                // Otherwise, return the preferences
                return Ok(response);
            }
            catch (Exception)
            {
                // If any other error occurs, return a bad request
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
    }
}
