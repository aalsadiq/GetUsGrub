using GitGrub.GetUsGrub.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    interface IUser:IPermission
    {
        //required fields should be added
        string GetType { get; }
        IEnumerable<Permission> GetPermission { get; }
        void AddPermission(Permission permission);
        void RemovePermission(Permission permission);
        Object GetLocation { get; }
        Object GetProfile { get; }
        string GetSecurityAnswer { get; }
        Boolean IsActive();
        int UserId();//added by salas
        int GetUserId { get; }//added by salas
    }
}
