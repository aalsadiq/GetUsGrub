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

        //convert string to user
        var stringToUser = usertojson(objectToString);

        //Conver string to user
        var user = jsontouser(stringToUser);

        //business rules...
        
            //validate if user exists by calling gateway...
       
            //validate if password is satisfies the requirements > 8 characters if valid call salt inside... if so hashpassword

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
        return false;
    }

    public Boolean validateUserType(User user)
    {
        //validate
        if(user.UserType == "admin" || user.UserType =="registered" || user.UserType == "restaurant")
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

