namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Menu item interface
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>
    public interface IMenuItem
    {
        string ItemName { get; set; }

        double ItemPrice { get; set; }

        string ItemPicture { get; set; }

        string ItemType { get; set; }

        string Description { get; set; }

        bool IsActive { get; set; }
    }
}
