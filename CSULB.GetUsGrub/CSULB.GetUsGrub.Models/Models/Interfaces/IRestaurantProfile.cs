namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant profile interface
    /// @author: Andrew Kao
    /// @updated: 3/20/18
    /// </summary>
    public interface IRestaurantProfile
    {
        string PhoneNumber { get; set; }
        Address Address { get; set; }
        RestaurantDetail Details { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
