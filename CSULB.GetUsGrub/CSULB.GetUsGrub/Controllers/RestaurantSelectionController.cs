using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// The <c>RestaurantSelectionController</c> class.
    /// Selects a restaurant for users.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/30/2018
    /// </para>
    /// </summary>
    [RoutePrefix("RestaurantSelection")]
    public class RestaurantSelectionController : ApiController
    {
        /// <summary>
        /// /// <summary>
        /// The UnregisteredUserRestaurantSelection method.
        /// Routes to entities that will select a restaurant for unregistered users.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/30/2018
        /// </para>
        /// </summary>
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="foodType"></param>
        /// <param name="distanceInMiles"></param>
        /// <param name="avgFoodPrice"></param>
        /// <returns>Ok HTTP response or Bad Request HTTP response</returns>
        [HttpGet]
        // Opts authentication
        [AllowAnonymous]
        [Route("Unregistered")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "GET")]
        public IHttpActionResult UnregisteredUserRestaurantSelection(string city, string state, string foodType, int distanceInMiles, int avgFoodPrice)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                // Instantiating dependencies
                var restaurantSelectionDto = new RestaurantSelectionDto(city: city, state: state, foodType: foodType, distanceInMiles: distanceInMiles, avgFoodPrice: avgFoodPrice);
                var restaurantSelectionManager = new UnregisteredUserRestaurantSelectionManager(restaurantSelectionDto);

                // Select a restaurant
                var response = restaurantSelectionManager.SelectRestaurant();
                if (response.Error != null)
                {
                    // Sending HTTP response 400 Status
                    return BadRequest(response.Error);
                }

                // Sending HTTP response 200 Status
                return Ok(response.Data);
            }
            // Catch exceptions
            catch (Exception)
            {
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }

        /// <summary>
        /// /// <summary>
        /// The RegisteredUserRestaurantSelection method.
        /// Routes to entities that will select a restaurant for registered users.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/04/2018
        /// </para>
        /// </summary>
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="foodType"></param>
        /// <param name="distanceInMiles"></param>
        /// <param name="avgFoodPrice"></param>
        /// <returns>Ok HTTP response or Bad Request HTTP response</returns>
        [HttpGet]
        [Route("Registered")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "GET")]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.RESTAURANTSELECTION, Operation = ActionConstant.READ)]
        public IHttpActionResult RegisteredUserRestaurantSelection(string city, string state, string foodType, int distanceInMiles, int avgFoodPrice)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                // Instantiating dependencies
                var restaurantSelectionDto = new RestaurantSelectionDto(city: city, state: state, foodType: foodType, distanceInMiles: distanceInMiles, avgFoodPrice: avgFoodPrice);
                var restaurantSelectionManager = new RegisteredUserRestaurantSelectionManager(restaurantSelectionDto: restaurantSelectionDto, token: Request.Headers.Authorization.Parameter);
 
                // Select a restaurant
                var response = restaurantSelectionManager.SelectRestaurant();
                if (response.Error != null)
                {
                    // Sending HTTP response 400 Status
                    return BadRequest(response.Error);
                }

                // Sending HTTP response 200 Status
                return Ok(response.Data);
            }
            // Catch exceptions
            catch (Exception)
            {
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
    }
}
