namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Interface for an address in the United States.
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
