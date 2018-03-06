namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The ISalt interface.
    /// A contract with defined property for classes with Salts.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public interface ISalt
    {
        string Salt { get; set; }
    }
}
