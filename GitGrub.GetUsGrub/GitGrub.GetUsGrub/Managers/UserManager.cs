using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitGrub.GetUsGrub.Models.Models;
using System.Runtime.Serialization.Json;//for DataContractJsonSerializerClass
using System.IO;
using System.Text;
using Newtonsoft.Json;//what?

public class UserManager
    {

    public User createUser (Object user)
    {
        //convert object to string
       var userstring = Convert.ToString(user);

        //validate username
        //validate userpassword
        //validate that the type suggested is true
        //validate security question
        //talk to Jen, are we implementing this?










        //convert object to json
        var userobject = usertojson(userstring);
        //check business rules

       
        // return the user
        var test1 = jsontouser(userobject);
        return test1;
    }

    public void deleteUser (Object user)
    {
        //convert object to json

        //check business rules
        //validate if user exists in the db if not then do nothing...

        
        var test1 = new User();
    }

    public void deactivateUser(Object user)
    {

    }
    public void reactivateUser(Object user)
    {

    }
    public User editUser(Object user)
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
    public static User jsontouser(string user)
    {
        var uservalue = JsonConvert.DeserializeObject<User>(user);
        
        return uservalue;
    }


    //Serialize - Object to JSON
    public static string usertojson(Object user)
    {
        string json = JsonConvert.SerializeObject(user);
        return json;
    }







}

