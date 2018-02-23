using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant user profile class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public class RestaurantProfile : IProfile, IRestaurantProfile
    {
        public int Id { get; set; }
        
        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }

        public IRestaurantDetail Details { get; set; }

        public IList<IRestaurantMenu> Menu { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
