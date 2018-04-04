namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Constant class for Action Types
    /// 
    /// @author: Rachel Dang
    /// @updated: 3/20/18
    /// </summary>
    public class ActionConstant
    {
        // General CRUD Action Types
        public const string CREATE = "Create";
        public const string READ = "Read";
        public const string UPDATE = "Update";
        public const string DELETE = "Delete";

        // Action Types specifically for Profile/Restaurant Profile Management
        public const string DEACTIVATE = "Deactivate";
        public const string REACTIVATE = "Reactivate";

        // Action Types specifically for Food Preferences
        public const string SUGGEST = "Suggest";

        // Action Type specifically for Bill Splitter & Restaurant Selector
        public const string ACCESS = "Access";
    }
}
