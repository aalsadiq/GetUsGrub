using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitGrub.GetUsGrub.Models;
namespace GitGrub.GetUsGrub.Models
{
    public class Permission:IPermission 
    {
        public string PermissionName { get; set; }
        public string PermissionType { get; set; }
        public string ContextType { get; set; }
        public string PossessionType { get; set; }

        public string GetActionType()
        {
            return "";
        }

        public string GetContextType()
        {
            return "";
        }

        public string GetName()
        {
            return "";
        }

        public string GetPossessionType()
        {
            return "";
        }
    }
}