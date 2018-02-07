using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Models
{
    interface IUser
    {
        string GetType();
        IEnumerable<Permission> GetPermission();
        void  AddPermission(Permission permission);
        void RemovePermission(Permission permission);
        Object GetLocation();
        Object GetProfile();
        string GetSecurityAnswer();
        Boolean IsActive();
        int UserId();//added by salas
        int GetUserId();//added by salas

    }
}
