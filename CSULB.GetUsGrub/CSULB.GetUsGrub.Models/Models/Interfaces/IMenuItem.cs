namespace CSULB.GetUsGrub.Models
{
    public interface IMenuItem
    {
        int PublicItemId { get; set; }

        string ItemName { get; set; }

        double ItemPrice { get; set; }

        string ItemPicture { get; set; }

        string Tag { get; set; }

        string Description { get; set; }

        bool IsActive { get; set; }
    }
}
