namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantProfileDto</c> class.
    /// Defines properties pertaining to a data transfer object of a restaurant profile.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileDto : IRestaurantProfile
    {
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public RestaurantDetail Details { get; set; }
    }
}