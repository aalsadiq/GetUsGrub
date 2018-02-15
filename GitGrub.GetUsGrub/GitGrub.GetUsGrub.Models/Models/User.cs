using System;
using System.Collections.Generic;
using System.Text;
using GitGrub.GetUsGrub.Models;//must include for models
using GitGrub.GetUsGrub.Models.Interfaces;//must include for interfaces
using System.ComponentModel.DataAnnotations; //from entity framework

namespace GitGrub.GetUsGrub.Models.Models
{
    public class User:IUser
    {
        //username
        private String _username;
        //userid
        private int _userId;
        //userpassword
        private String _userPassword;
        //usertype
        private String _userType;
       //securityAnswer
        private String _securityAnswer;
        //Permissions
        private IEnumerable<Permission> _permissions;
        //Locations
        private Object _location;
        //Profile
        private Object _profile;
        //Active
        private Boolean _active;

        [Key]
        [Required(ErrorMessage = "User information is missing.")]
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        [Required]
        [MinLength(1, ErrorMessage = "Answer must have at least 1 character.")]
        public string SecurityAnswer
        {
            get { return _securityAnswer; }
            set { _securityAnswer = value; }
        }

        public IEnumerable<Permission> Permission
        {
            get { return _permissions; }
            set { _permissions = value; }
        }

        [Key]
        [Required(ErrorMessage = "User information is missing.")]
        [MinLength(8, ErrorMessage = "Password must 8 or more characters long")]
        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }
        public object Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public object Profile
        {
            get { return _profile; }
            set { _profile = value; }
        }

        private string _name;
        private string _actionType;
        private string _contextType;
        private string _possessionType;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string ActionType
        {
            get { return _actionType; }
            set { _actionType = value; }
        }
        public string ContextType
        {
            get { return _contextType; }
            set { _contextType = value; }
        }
        public string PossessionType
        {
            get { return _possessionType; }
            set { _possessionType = value; }
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
