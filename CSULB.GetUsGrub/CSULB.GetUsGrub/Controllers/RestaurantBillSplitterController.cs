using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.IdentityModel.Services;
using System.Security.Permissions;
using System.Web.Http;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// Retreives information from a restaurant by its public restaurant ID
    /// and returns the Menus and MenuItems of each Menu that specific restaurant has.
    /// @author Ryan Luong
    /// @updated 4/4/18
    /// </summary>
    [RoutePrefix("api/v1/RestaurantBillSplitter")]
    public class RestaurantBillSplitterController : ApiController
    {
        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.MENU, Operation = ActionConstant.ACCESS)]
        [Route("Restaurant")]
        public IHttpActionResult GetRestaurantMenus(int restaurantId)
        {
            var restaurantDto = new RestaurantDto(restaurantId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var restaurantBillSplitterManager = new RestaurantBillSplitterManager();
                var response = restaurantBillSplitterManager.GetRestaurantMenus(restaurantDto.RestaurantId);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}