using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// An object for individual user registration using the Parameter Object design pattern
    /// 
    /// <para>
    /// @author: Brian Fann
    /// @updated: 4/25/18
    /// </para>
    /// </summary>
    public class RestaurantRegistrationParameterObject
    {
        public UserAccount UserAccount { get; set; }
        public List<SecurityQuestion> SecurityQuestions { get; set; }
        public List<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }
        public PasswordSalt PasswordSalt { get; set; }
        public UserClaims UserClaims { get; set; }
        public UserProfile UserProfile { get; set; }
        public RestaurantProfile RestaurantProfile { get; set; }
        public List<BusinessHour> BusinessHours { get; set; }
        public List<FoodPreference> FoodPreferences { get; set; }
    }
}
