using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;//for keys
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Models
{
    public class User : IUser
    {
        [Key]
        private string Username { get; set; }
        private Type UserType { get; set; }
        private string SecurityAnswer { get; set; }
        private IEnumerable<Permission> Permissions { get; set; }
        private Object Location { get; set; }
        private Object profile { get; set; }
        private Boolean Active { get; set; }

        public void AddPermission(Permission permission)
        {
            throw new NotImplementedException();
        }

        public object GetLocation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Permission> GetPermission()
        {
            throw new NotImplementedException();
        }

        public object GetProfile()
        {
            throw new NotImplementedException();
        }

        public string GetSecurityAnswer()
        {
            throw new NotImplementedException();
        }

        public int GetUserId()
        {
            throw new NotImplementedException();
        }

        public bool IsActive()
        {
            throw new NotImplementedException();
        }

        public void RemovePermission(Permission permission)
        {
            throw new NotImplementedException();
        }

        //added by salas
        public int UserId()
        {
            throw new NotImplementedException();
        }
        //add by salas
        string IUser.GetType()
        {
            throw new NotImplementedException();
        }
    }
}