using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{
		[RoutePrefix("RestaurantBillSplitter")]
		public class RestaurantBillSplitterController : ApiController
		{
				[HttpGet]
				[AllowAnonymous] // TODO: Remove for deployment
				[Route("Restaurant")]
				[EnableCors(origins: "http://localhost:8081", headers: "*", methods: "*")]
				public IHttpActionResult GetRestaurantMenus([FromBody] RestaurantMenuDto restaurantMenuDto)
				{
						if (!ModelState.IsValid)
						{
								return BadRequest(ModelState);
						}

						try
						{
								var restaurantBillSplitterManager = new RestaurantBillSplitterManager();
								var response = restaurantBillSplitterManager.GetRestaurantMenus(restaurantMenuDto.DisplayName, restaurantMenuDto.Latitude, restaurantMenuDto.Longitude);
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
		}
}