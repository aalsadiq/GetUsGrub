namespace CSULB.GetUsGrub.DataAccess.Migrations.UserManagement.UserContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBSeedV1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("GetUsGrub.RestaurantProfile", "Details_HasReservations", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_HasDelivery", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_HasTakeOut", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_AcceptCreditCards", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_Attire", c => c.String());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_ServesAlcohol", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_HasOutdoorSeating", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_HasTv", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_HasDriveThru", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_Caters", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_AllowsPets", c => c.Boolean());
            AddColumn("GetUsGrub.RestaurantProfile", "Details_Category", c => c.String());
            DropColumn("GetUsGrub.RestaurantProfile", "DisplayName");
            DropColumn("GetUsGrub.RestaurantProfile", "DisplayPicture");
        }
        
        public override void Down()
        {
            AddColumn("GetUsGrub.RestaurantProfile", "DisplayPicture", c => c.String());
            AddColumn("GetUsGrub.RestaurantProfile", "DisplayName", c => c.String());
            DropColumn("GetUsGrub.RestaurantProfile", "Details_Category");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_AllowsPets");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_Caters");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_HasDriveThru");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_HasTv");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_HasOutdoorSeating");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_ServesAlcohol");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_Attire");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_AcceptCreditCards");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_HasTakeOut");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_HasDelivery");
            DropColumn("GetUsGrub.RestaurantProfile", "Details_HasReservations");
        }
    }
}
