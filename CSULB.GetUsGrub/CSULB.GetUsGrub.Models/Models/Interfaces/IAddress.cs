namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Address interface
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/20/18
    /// </summary>
    public interface IAddress
    {
        string Street1 { get; set; }
        string Street2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        int Zip { get; set; }
    }
}
