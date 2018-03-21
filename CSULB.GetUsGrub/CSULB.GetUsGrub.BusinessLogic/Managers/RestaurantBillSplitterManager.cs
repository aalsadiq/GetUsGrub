using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
		public class RestaurantBillSplitterManager
		{
				public ResponseDto<Dictionary<RestaurantMenu, IList<RestaurantMenuItem>>> GetRestaurantMenus(string displayName, double latitude, double longitude) {

						var restaurantBillSplitterGateway = new RestaurantBillSplitterGateway();

						var responseDtoFromGateway = restaurantBillSplitterGateway.GetRestaurantMenus(displayName, latitude, longitude);

						return responseDtoFromGateway;
				}
		}
}
