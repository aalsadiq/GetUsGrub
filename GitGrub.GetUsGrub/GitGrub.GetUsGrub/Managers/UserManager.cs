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

public class UserManager
    {

    public User createUser (Object json)
    {
        //convert object to string
       var objectToString = Convert.ToString(json);

        //convert strin to user
        var stringToUser = usertojson(objectToString);

        //Conver string to user
        var user = jsontouser(stringToUser);

        //business rules

        //validate user type
        //using enum to declare these types...
        if(user.Type == "regular")
        {
            //set regular user permissions
            //call validate
        }else if(user.Type == "restaurant")
        {
            //set restaurant permissions
        }
        else if(user.Type == "admin")
        {
            //set all permissions
            //can do crud
        }
        //validate username
        var holdUserName = user.name;

       //validateUserType(user);

        //validate userpassword
        //validate that the type suggested is true
        //validate security question
        //talk to Jen, are we implementing this?
        //then return this back to the controller.... which will give it to the gateway...

        return user;
    }

    public Boolean doesUserExist(User user)
    {
        //call UserGateway
        //does the user exist? if so it will return boolean true, else false
        //create UserExists
        return false;
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
        if(user.SecurityAnswer.Length > 1)//is it the same thing as not null
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean validateUserName(User user)
    {

        if(user.Username.Length > 8)//must be greater than 8 characters
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
        //validate
        if(user.Type == "admin" || user.Type =="registered" || user.Type == "restaurant")
        {

        }
        return false;
    }

    public Boolean validateUserLocation(User user)
    {
        return false;
    }

    public Boolean validateUserProfile (User user)
    {
        return false;
    }


    public Boolean validateActive(User user)
    {
        if (user.IsActive() == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


  //controller calls this method..
    public void deleteUser (Object json)
    {
        //convert object to string
        var objectToString = Convert.ToString(json);

        //convert strin to user
        var stringToUser = usertojson(objectToString);

        //Conver string to user
        var user = jsontouser(stringToUser);

  
    }

    //controller calls this method..
    public User deactivateUser(Object json)
    {
        //convert object to string
        var objectToString = Convert.ToString(json);

        //convert strin to user
        var stringToUser = usertojson(objectToString);

        //Conver string to user
        var user = jsontouser(stringToUser);
        //call business logic
        return user;
    }
    public User reactivateUser(User json)
    {
        //convert object to string
        var objectToString = Convert.ToString(json);

        //convert strin to user
        var stringToUser = usertojson(objectToString);

        //Conver string to user
        var user = jsontouser(stringToUser);
        //call business logic
        return user;
    }
    public User editUser(User user)
    {

        var test1 = new User();

        return test1;
    }
    public IEnumerable<User> GetAll()
    {
        //return all users from database...
        return GetAll();
    }

    //Deserialize - JSON to Object
    public  User jsontouser(string user)
    {
        var uservalue = JsonConvert.DeserializeObject<User>(user);
        
        return uservalue;
    }
    //Serialize - Object to JSON
    public  string usertojson(Object user)
    {
        string json = JsonConvert.SerializeObject(user);
        return json;
    }
}

