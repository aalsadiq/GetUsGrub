namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Constant class for Resource Types
    /// 
    /// @author: Rachel Dang
    /// @updated: 3/20/18
    /// </summary>
    public class ResourceConstant
    {
        // Resource Types for First Time Users
        public const string FIRSTTIMEUSER = "IsFirstTimeUser";

        // Resource Types for User Management
        public const string USER = "User";

        // Resource Types for Profile Management
        public const string INDIVIDUAL = "UserProfile";
        public const string RESTAURANT = "RestaurantProfile";

        // Resource Types for Food Preferences
        public const string PREFERENCES = "Preferences";

        // Resource Types for Bill Splitter
        public const string MENU = "Menu";
        public const string DICTIONARY = "Dictionary";

        // Resource Types for Restaurant Selection
        public const string RESTAURANTSELECTION = "RestaurantSelection";

        // Resource Types for Username
        public const string USERNAME = "Username";
    }
}
