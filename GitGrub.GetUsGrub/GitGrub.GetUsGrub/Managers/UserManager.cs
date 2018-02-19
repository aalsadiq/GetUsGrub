using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitGrub.GetUsGrub.Models.Models;
using System.Runtime.Serialization.Json;//for DataContractJsonSerializerClass
using System.IO;
using System.Text;
using Newtonsoft.Json;//what?
using GitGrub.GetUsGrub.Controllers;
using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models.DTOs;
using GitGrub.GetUsGrub.Interfaces;

public class UserManager
    {
    UserGateway userGateway = new UserGateway();
    public bool createUser(UserManagerDTO userDTO)
    {
        //validation of username is done by this method...
        //validation of userpassword is done in this method...
        //call gateway to check if user already exits if not it will create the user
        var validateUserNameResult = validateUserName(userDTO);
        if (validateUserNameResult == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        //return true;
    }

    public bool deleteUser(UserManagerDTO userDTO)
    {

        if (userGateway.DeleteUserByUsernameGateway(userDTO) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool reactivateUser(UserManagerDTO userDTO)
    {
        if (userGateway.ReactivateUserByUsernameGateway(userDTO) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool deactivateUser(UserManagerDTO userDTO)
    {
        if (userGateway.DeactivateUserByUsernameGateway(userDTO) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool editUser(UserManagerDTO userDTO)
    {
        if (userGateway.EditUserByUsernameGateway(userDTO) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool validateUserName(UserManagerDTO userDTO)
    {
        if (userDTO.UserName.Length > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool validateUserPassword(UserManagerDTO userDTO)
    {
        //validate password (is it as long as it should be)
        if (userDTO.HashedPassword.Length > 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

