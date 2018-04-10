using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSULB.GetUsGrub.Controllers
{
    public class FoodPreferencesController : ApiController
    {
        public IHttpActionResult GetPreferences([FromBody] string username)
        {
            return Ok();
        }
    }
}
