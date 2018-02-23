namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Interface representing a menu item
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/22/18
    /// </summary>
    public interface IMenuItem
    {
        string ItemName { get; set; }

        double ItemPrice { get; set; }

        string ItemPicture { get; set; }

        string Tag { get; set; }

        string Description { get; set; }

        bool IsActive { get; set; }
    }
}
