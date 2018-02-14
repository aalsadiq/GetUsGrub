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
        public string PossessionType { get; set; }

        public string GetName => throw new NotImplementedException();

        public string GetActionType => throw new NotImplementedException();

        public string GetContextType => throw new NotImplementedException();

        public string GetPossessionType => throw new NotImplementedException();
    }
}
