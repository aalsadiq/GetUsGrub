using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Managers
{
    public class ProfileManager
    {
        public bool EditRegularProfile(RegularProfile regularProfile)
        {
            // call edit user gateway

            return true;
        }

        public bool EditRestaurantProfile(RestaurantProfile restaurantProfile)
        {
            // call edit user gateway
            return true;
        }
    }
}