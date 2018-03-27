namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The IRestaurantProfile interface.
    /// A contract with defined properties for the RestaurantProfile class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    public interface IRestaurantProfile
    {
        string PhoneNumber { get; }
        Address Address { get; }
        double Longitude { get; }
        double Latitude { get; }
        RestaurantDetail Details { get; }
    }
}
