using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models;//must include for models
using GitGrub.GetUsGrub.Models.Interfaces;//must include for interfaces

namespace GitGrub.GetUsGrub.Models.Models
{
    class User:IUser
    {
        //required fields should be added
        public IEnumerable<Permission> GetPermission => throw new NotImplementedException();

        public object GetLocation => throw new NotImplementedException();

        public object GetProfile => throw new NotImplementedException();

        public string GetSecurityAnswer => throw new NotImplementedException();

        public int GetUserId => throw new NotImplementedException();

        public string GetName => throw new NotImplementedException();

        public string GetActionType => throw new NotImplementedException();

        public string GetContextType => throw new NotImplementedException();

        public string GetPossessionType => throw new NotImplementedException();

        private string Username { get; set; }
        private Type UserType { get; set; }
        private string SecurityAnswer { get; set; }
        private IEnumerable<Permission> Permissions { get; set; }
        private Object Location { get; set; }
        private Object profile { get; set; }
        private Boolean Active { get; set; }

        string IUser.GetType => throw new NotImplementedException();

        public void AddPermission(Permission permission)
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

        public int UserId()
        {
            throw new NotImplementedException();
        }
    }
}
