using System.Data.Entity;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// Context used for food selection + restaurant user profile management.
    /// Context that allows connections to specific entities for restaurant selection and restaurant profile management.
    /// 
    /// Brian Fann
    /// 2/21/18
    /// </summary>

    public class RestaurantContext : DbContext
    {
        public RestaurantContext() : base("GetUsGrub") { }
    }

}
