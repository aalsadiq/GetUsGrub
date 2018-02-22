namespace GitGrub.GetUsGrub.Models
{
    public interface IMenuItem
    {
        int Id { get; set; }

        string ItemName { get; set; }

        double ItemPrice { get; set; }

        string ItemPicture { get; set; }

        string Tag { get; set; }

        string Description { get; set; }

        bool IsActive { get; set; }
    }
}
