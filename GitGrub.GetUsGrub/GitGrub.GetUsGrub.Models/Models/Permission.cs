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
        public string PermissionName { get; set; }
        public string PermissionType { get; set; }
        public string ContextType { get; set; }

        public string contexttype => throw new NotImplementedException();

        public string PossessionType { get; set; }

        public string possessiontype => throw new NotImplementedException();

        public string name => throw new NotImplementedException();

        public string actiontype => throw new NotImplementedException();
    }
}
