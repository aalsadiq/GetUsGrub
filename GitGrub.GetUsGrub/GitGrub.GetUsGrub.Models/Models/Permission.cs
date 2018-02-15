using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models;//must include for models
using GitGrub.GetUsGrub.Models.Interfaces;//must include for interfaces

namespace GitGrub.GetUsGrub.Models.Models
{
    public class Permission:IPermission
    {
        //required fields should be added
        private String _permissionName { get; set; }
        private String _actionType { get; set; }
        private String _permissionType { get; set; }
        private String _contextType { get; set; }

        public String Name
        {
            get { return _permissionName; }
            set { _permissionName = value; }
        }
        public String ActionType
        {
            get { return _actionType; }
            set { _actionType = value; }
        }
        public String PossessionType
        {
            get { return _permissionType; }
            set { _permissionName = value; }
        }
        public String ContextType
        {
            get { return _contextType; }
            set { _contextType = value; }
        }
    }
}
