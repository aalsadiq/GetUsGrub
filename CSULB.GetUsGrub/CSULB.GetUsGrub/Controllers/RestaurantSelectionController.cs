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
        /// <param name="distance"></param>
        /// <param name="avgFoodPrice"></param>
        /// <returns>Ok HTTP response or Bad Request HTTP response</returns>
        [HttpGet]
        // Opts authentication
        [AllowAnonymous]
        [Route("Unregistered")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "GET")]
        public IHttpActionResult UnregisteredUserRestaurantSelection(string city, string state, string foodType, int distance, int avgFoodPrice)
        {
            var restaurantSelectionDto = new RestaurantSelectionDto(city: city, state: state, foodType: foodType, distance: distance, avgFoodPrice: avgFoodPrice);
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                // TODO: @Jenn Parse the ModelState BadRequest to something better [-Jenn]
                return BadRequest(ModelState);
            }
            try
            {
                var restaurantSelectionManager = new RestaurantSelectionManager();
                var response = restaurantSelectionManager.SelectRestaurantWithoutFoodPreferences(restaurantSelectionDto);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                // Sending HTTP response 200 Status
                return Ok(response.Data);
            }
            // Catch exceptions
            catch (Exception)
            {
                // Sending HTTP response 400 Status
                return BadRequest("Something went wrong. Please try again later.");
            }
        }
    }
}
