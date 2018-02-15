using GitGrub.GetUsGrub.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface IUser:IPermission
    {
        //required fields should be added

        int UserId { get; set; }
        string UserPassword { get; set; }
        string UserType { get; set; }//type get and set
        IEnumerable<Permission> Permission { get; set; } //creates getpermission, sets permission
        void RemovePermission(Permission Permission);//remove permission
        Object Location { get; set; }//getlocation , setlocation
        Object Profile { get; set; }//geprofile, setprofile
        string SecurityAnswer { get; set; }//get securityAnswer, setSecurityAnswer
        Boolean IsActive();


    }
}