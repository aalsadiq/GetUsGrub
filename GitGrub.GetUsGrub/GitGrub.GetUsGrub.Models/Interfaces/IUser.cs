using GitGrub.GetUsGrub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface IUser:IPermission
    {
        //required fields should be added
        string Type { get; set; }//creates get type
        IEnumerable<Permission> Permission { get; set; } //creates getpermission, sets permission
        void AddPermission(Permission Permission);//?
        void RemovePermission(Permission Permission);//remove permission
        Object Location { get; set; }
        Object Profile { get; set; }
        string SecurityAnswer { get; set; }
        Boolean IsActive();
        int UserId { get; set; }
    }
}
