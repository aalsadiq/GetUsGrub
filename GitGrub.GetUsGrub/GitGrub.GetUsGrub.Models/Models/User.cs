using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models;//must include for models
using GitGrub.GetUsGrub.Models.Interfaces;//must include for interfaces

namespace GitGrub.GetUsGrub.Models.Models
{
    public class User:IUser
    {
        public string Username;
        public Type UserType;
        public string SecurityAnswer;
        public IEnumerable<Permission> Permissions;
        public Object Location;
        public Object profile;
        public Boolean Active;

        public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<Permission> Permission { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Profile { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string name => throw new NotImplementedException();

        public string actiontype => throw new NotImplementedException();

        public string contexttype => throw new NotImplementedException();

        public string possessiontype => throw new NotImplementedException();

        object IUser.Location { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IUser.SecurityAnswer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddPermission(Permission Permission)
        {
            throw new NotImplementedException();
        }

        public bool IsActive()
        {
            throw new NotImplementedException();
        }

        public void RemovePermission(Permission Permission)
        {
            throw new NotImplementedException();
        }
    }
}
