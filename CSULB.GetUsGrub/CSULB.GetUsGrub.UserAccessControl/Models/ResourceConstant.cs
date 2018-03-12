using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Constant class for Resource Types
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 2/21/18
    /// </summary>
    public class ResourceConstant
    {
        // Resource Types for User Management
        public static string RESOURCETYPE_USER = "User";

        // Resource Types for Profile Management
        public static string RESOURCETYPE_PROFILE = "UserProfile";
        public static string RESOURCETYPE_RESTAURANTPROFILE = "RestaurantProfile";

        // Resource Types for Food Preferences
        public static string RESOURCETYPE_PREFERENCES = "Preferences";

        // Resource Types for Bill Splitter
        public static string RESOURCETYPE_MENU = "Menu";
        public static string RESOURCETYPE_DICTIONARY = "Dictionary";
    }
}
