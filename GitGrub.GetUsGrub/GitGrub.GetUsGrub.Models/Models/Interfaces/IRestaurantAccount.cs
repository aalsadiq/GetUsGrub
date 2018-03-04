namespace GitGrub.GetUsGrub.Models
{
    public interface IRestaurantAccount : IAddress, IBusinessHours
    {
        string PhoneNumber { get; set; }
    }
}
