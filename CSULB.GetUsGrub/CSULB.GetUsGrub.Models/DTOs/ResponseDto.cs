namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>ResponseDto</c> class.
    /// Defines properties for a ResponseDto
    /// <para>
    /// @author: Andrew Kao, Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
    }
}