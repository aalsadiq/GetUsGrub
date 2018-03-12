namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>Address</c> class.
    /// Defines properties pertaining to an address.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    [System.Serializable]
    public class Address
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
    }
}
