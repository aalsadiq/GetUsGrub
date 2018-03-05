namespace GitGrub.GetUsGrub.Models
{
    public interface IRegisterRestaurantUserDto : IRegisterUserDto
    {
        RestaurantAccount RestaurantAccount { get; set; }
    }
}
