namespace CSULB.GetUsGrub.Models
{
    public class ImagePaths
    {
        // Default virtual image root path
        public const string DEFAULT_VIRTUAL_ROOT_PATH = "http://localhost:64525/Images/";

        // Default virtual images
        public const string DEFAULT_VIRTUAL_MENU_ITEM_PATH = DEFAULT_VIRTUAL_ROOT_PATH + "DefaultImages/DefaultMenuItemImage.png"; 
        public const string DEFAULT_VIRTUAL_DISPLAY_IMAGE_PATH = DEFAULT_VIRTUAL_ROOT_PATH + "DefaultImages/DefaultProfileImage.png";

        // Default virtual image path
        public const string VIRTUAL_PROFILE_IMAGE_PATH = DEFAULT_VIRTUAL_ROOT_PATH + "ProfileImages/";
        public const string VIRTUAL_MENU_ITEM_PATH = DEFAULT_VIRTUAL_ROOT_PATH + "MenuImages/";

        // Default physical image root path
        public const string DEFAULT_PHYSICAL_ROOT_PATH = @"C:\Users\Angelica\Documents\GetUsGrub\CSULB.GetUsGrub\CSULB.GetUsGrub.Images\Images\";

        // Default physical images
        public const string DEFAULT_PHYSICAL_MENU_ITEM_PATH = DEFAULT_PHYSICAL_ROOT_PATH + @"DefaultImages\DefaultMenuItemImage.png";
        public const string DEFAULT_PHSYICAL_DISPLAY_IMAGE_PATH = DEFAULT_PHYSICAL_ROOT_PATH + @"DefaultImages\DefaultProfileImage.png";

        // Default physical image path
        public const string PHSYICAL_PROFILE_IMAGE_PATH = DEFAULT_PHYSICAL_ROOT_PATH + @"ProfileImages\";
        public const string PHYSICAL_MENU_ITEM_PATH = DEFAULT_PHYSICAL_ROOT_PATH + @"MenuImages\";

    }
}
