//using CSULB.GetUsGrub.BusinessLogic;
//using CSULB.GetUsGrub.Models;
//using System;
//using System.Web.Http;
//using System.Web.Http.Cors;

//namespace CSULB.GetUsGrub.Controllers
//{
//    /// <summary>
//    /// Restaurant profile controller
//    /// 
//    /// @author: Andrew Kao
//    /// @updated: 3/18/18
//    /// </summary> 
//    [RoutePrefix("Profile/User")]
//    public class RestaurantProfileController : ApiController
//    {
//        [HttpGet]
//        [AllowAnonymous] // TODO: Remove for deployment
//        [Route("Restaurant")]
//        [EnableCors(origins: "http://localhost:8081", headers: "*", methods: "*")]
//        public IHttpActionResult GetProfile([FromBody] string username)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var profileManager = new RestaurantProfileManager();
//                var response = profileManager.GetProfile(username);
//                if (response.Error != null)
//                {
//                    return BadRequest(response.Error);
//                }

//                return Ok(response);
//            }

//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }

//        [HttpPut]
//        [AllowAnonymous] // TODO: Remove for deployment
//        [Route("Restaurant/Edit")]
//        [EnableCors(origins: "http://localhost:8081", headers: "*", methods: "*")]
//        public IHttpActionResult EditProfile([FromBody] RestaurantProfileDto restaurantProfileDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var profileManager = new RestaurantProfileManager();
//                var response = profileManager.EditProfile(restaurantProfileDto);
//                if (response.Error != null)
//                {
//                    return BadRequest(response.Error);
//                }

//                return Ok(response);
//            }

//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }
//    }
//}
