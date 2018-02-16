using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models;//must include for models

namespace GitGrub.GetUsGrub.Models.Models
{
    public class Permission
    {
        public string PermissionName { get; set; }
        public string PermissionActionType { get; set; }
        public string PermissionContextType { get; set; }
        public string PermissionPossessionType { get; set; }

        //constructor

    }
}
