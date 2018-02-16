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

public class UserManager
    {

    //Creating the service that will handle the business logic (Use rManager.cs)
    public UserGateway UserGateway { get; }

    //create a controller for the service...
    public UserManager()
    {
    }
    //create a controller for the service...
    public UserManager(UserGateway userGateway)
    {
        UserGateway = userGateway;
    }

    public User createUser (User user)
    {
       // //convert object to string
       //var objectToString = Convert.ToString(json);

       // //convert string to user
       // var stringToUser = usertojson(objectToString);

       // //Conver string to user
       // var user = jsontouser(stringToUser);

        //business rules...

        //validate if user exists by calling gateway...
       if( doesUserExist(user) == false)
        {
            //move on, else exit out and display error message...
        }

        //validate if password is satisfies the requirements > 8 characters if valid call salt inside... if so hashpassword
        if (validateUserName(user) == true)
        {

        }
        //validate security answer, must be > 1

        //validate if usertype is a valid type in our system... if so set the permission

        //End of business rules...

        return user;
    }
    
    public Boolean doesUserExist(User user)
    {
        //call UserGateway
        //does the user exist? if so it will return boolean true, else false
        //create UserExists

        if (UserGateway.CreateUserGateway(user) == true)
        {
            //create user...
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean validatePassword(User user)
    {

        //if user satisfies the requirements then continue
        //if gateway returns false then return false;
        return false;

    }

    //will check if user has a null security answer
    public Boolean validateSecurityAnswer(User user)
    {
        //if (user.SecurityAnswer.Length > 1)//is it the same thing as not null
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
        return false;
    }

    public Boolean validateUserName(User user)
    {

        if (user.UserName.Length > 8)//must be greater than 8 characters
        {
            //checking characters 
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean validateUserType(User user)
    {

        return false;
    }

    public Boolean validateUserLocation(User user)
    {
        //call map api to validate...
        return false;
    }

    public Boolean validateUserProfile(User user)
    {
        //? not sure how to validate a userprofile... what does it contain... ask andrew..
        return false;
    }


    public Boolean validateActive(User user)
    {
        //if (user.IsActive() == true)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
        return false;

    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //controller calls this method..
    public void deleteUser(User user)
    {
        ////convert object to string
        //var objectToString = Convert.ToString(json);

        ////convert strin to user
        //var stringToUser = usertojson(objectToString);

        ////Conver string to user
        //var user = jsontouser(stringToUser);
        
    }

    //controller calls this method..
    public User deactivateUser(User user)
    {
        //call business logic
        return user;
    }
    public User reactivateUser(User user)
    {
        ////convert object to string
        //var objectToString = Convert.ToString(user);

        ////convert strin to user
        //var stringToUser = usertojson(objectToString);

        //call business logic
        return user;
    }
    public User editUser(User user)
    {

        return user;
    }
    public IEnumerable<User> GetAll()
    {
        //return all users from database...
        return GetAll();
    }

    ////Deserialize - JSON to Object
    //public User jsontouser(string user)
    //{
    //    var uservalue = JsonConvert.DeserializeObject<User>(user);

    //    return uservalue;
    //}
    ////Serialize - Object to JSON
    //public string usertojson(Object user)
    //{
    //    string json = JsonConvert.SerializeObject(user);
    //    return json;
    //}
}

