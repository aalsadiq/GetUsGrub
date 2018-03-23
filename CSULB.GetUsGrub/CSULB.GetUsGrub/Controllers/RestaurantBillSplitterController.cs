using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace CSULB.GetUsGrub.Controllers
{
		[RoutePrefix("RestaurantBillSplitter")]
		public class RestaurantBillSplitterController : ApiController
		{
				[HttpGet]
				[AllowAnonymous] // TODO: Remove for deployment
				[Route("Restaurant")]
				[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "GET")]
				public IHttpActionResult GetRestaurantMenus(string displayName, double latitude, double longitude)
				{
						var restaurantDto = new RestaurantDto(displayName, latitude, longitude);

						if (!ModelState.IsValid)
						{
								return BadRequest(ModelState);
						}
						try
						{
								var restaurantBillSplitterManager = new RestaurantBillSplitterManager();
								Debug.WriteLine("Created Manager");
								Debug.WriteLine("Testing");
								var response = restaurantBillSplitterManager.GetRestaurantMenus(restaurantDto.DisplayName, restaurantDto.Latitude, restaurantDto.Longitude);
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