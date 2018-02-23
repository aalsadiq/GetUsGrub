namespace GitGrub.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Constant class for Action Types
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 2/21/18
    /// </summary>
    public class ActionConstant
    {
        // General CRUD Action Types
        public static string ACTIONTYPE_CREATE = "Create";
        public static string ACTIONTYPE_READ = "Read";
        public static string ACTIONTYPE_UPDATE = "Update";
        public static string ACTIONTYPE_DELETE = "Delete";

        // Action Types specifically for Profile/Restaurant Profile Management
        public static string ACTIONTYPE_DEACTIVATE = "Deactivate";
        public static string ACTIONTYPE_REACTIVATE = "Reactivate";

        // Action Types specifically for Food Preferences
        public static string ACTIONTYPE_SUGGEST = "Suggest";
    }
}