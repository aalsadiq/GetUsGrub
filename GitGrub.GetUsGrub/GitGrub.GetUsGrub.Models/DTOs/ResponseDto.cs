namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The IRegisterRestaurantUserDto interface.
    /// A contract with defined properties for the RegisterRestaurantUserDto class.
    /// <para>
    /// @author: Andrew Kao, Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class ResponseDto<T>
    {
        public T Data { get; set; }

        public string Error { get; set; }
    }
}
