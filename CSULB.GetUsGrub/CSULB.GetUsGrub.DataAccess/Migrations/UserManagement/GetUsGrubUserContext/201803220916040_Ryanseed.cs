namespace CSULB.GetUsGrub.DataAccess.Migrations.UserManagement.GetUsGrubUserContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ryanseed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "GetUsGrub.BusinessHour",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        Day = c.String(nullable: false),
                        OpenTime = c.String(nullable: false),
                        CloseTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.RestaurantProfile", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "GetUsGrub.RestaurantProfile",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Address_Street1 = c.String(nullable: false),
                        Address_Street2 = c.String(),
                        Address_City = c.String(nullable: false),
                        Address_State = c.String(nullable: false),
                        Address_Zip = c.Int(nullable: false),
                        Details_HasReservations = c.Boolean(),
                        Details_HasDelivery = c.Boolean(),
                        Details_HasTakeOut = c.Boolean(),
                        Details_AcceptCreditCards = c.Boolean(),
                        Details_Attire = c.String(),
                        Details_ServesAlcohol = c.Boolean(),
                        Details_HasOutdoorSeating = c.Boolean(),
                        Details_HasTv = c.Boolean(),
                        Details_HasDriveThru = c.Boolean(),
                        Details_Caters = c.Boolean(),
                        Details_AllowsPets = c.Boolean(),
                        Details_Category = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserProfile", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.RestaurantMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(),
                        MenuName = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.RestaurantProfile", t => t.RestaurantId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "GetUsGrub.RestaurantMenuItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(),
                        ItemName = c.String(),
                        ItemPrice = c.Double(nullable: false),
                        ItemPicture = c.String(),
                        Tag = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        RestaurantMenu_Id = c.Int(),
                        RestaurantMenu_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RestaurantMenu", t => t.MenuId)
                .ForeignKey("dbo.RestaurantMenu", t => t.RestaurantMenu_Id)
                .ForeignKey("dbo.RestaurantMenu", t => t.RestaurantMenu_Id1)
                .Index(t => t.MenuId)
                .Index(t => t.RestaurantMenu_Id)
                .Index(t => t.RestaurantMenu_Id1);
            
            CreateTable(
                "GetUsGrub.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DisplayPicture = c.String(),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "GetUsGrub.UserAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(),
                        IsFirstTimeUser = c.Boolean(),
                        RoleType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "GetUsGrub.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ClaimsJson = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "GetUsGrub.PasswordSalt",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "GetUsGrub.SecurityQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        Question = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserAccount", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "GetUsGrub.SecurityAnswerSalt",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.SecurityQuestion", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "GetUsGrub.Token",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TokenHeader = c.String(),
                        TokenSignature = c.String(),
                        Salt = c.String(),
                        IssuedOn = c.DateTime(nullable: false),
                        ExpiresOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("GetUsGrub.BusinessHour", "RestaurantId", "GetUsGrub.RestaurantProfile");
            DropForeignKey("GetUsGrub.RestaurantProfile", "Id", "GetUsGrub.UserProfile");
            DropForeignKey("GetUsGrub.UserProfile", "Id", "GetUsGrub.UserAccount");
            DropForeignKey("GetUsGrub.Token", "Id", "GetUsGrub.UserAccount");
            DropForeignKey("GetUsGrub.SecurityQuestion", "UserId", "GetUsGrub.UserAccount");
            DropForeignKey("GetUsGrub.SecurityAnswerSalt", "Id", "GetUsGrub.SecurityQuestion");
            DropForeignKey("GetUsGrub.PasswordSalt", "Id", "GetUsGrub.UserAccount");
            DropForeignKey("GetUsGrub.UserClaims", "Id", "GetUsGrub.UserAccount");
            DropForeignKey("dbo.RestaurantMenu", "RestaurantId", "GetUsGrub.RestaurantProfile");
            DropForeignKey("GetUsGrub.RestaurantMenuItem", "RestaurantMenu_Id1", "dbo.RestaurantMenu");
            DropForeignKey("GetUsGrub.RestaurantMenuItem", "RestaurantMenu_Id", "dbo.RestaurantMenu");
            DropForeignKey("GetUsGrub.RestaurantMenuItem", "MenuId", "dbo.RestaurantMenu");
            DropIndex("GetUsGrub.Token", new[] { "Id" });
            DropIndex("GetUsGrub.SecurityAnswerSalt", new[] { "Id" });
            DropIndex("GetUsGrub.SecurityQuestion", new[] { "UserId" });
            DropIndex("GetUsGrub.PasswordSalt", new[] { "Id" });
            DropIndex("GetUsGrub.UserClaims", new[] { "Id" });
            DropIndex("GetUsGrub.UserProfile", new[] { "Id" });
            DropIndex("GetUsGrub.RestaurantMenuItem", new[] { "RestaurantMenu_Id1" });
            DropIndex("GetUsGrub.RestaurantMenuItem", new[] { "RestaurantMenu_Id" });
            DropIndex("GetUsGrub.RestaurantMenuItem", new[] { "MenuId" });
            DropIndex("dbo.RestaurantMenu", new[] { "RestaurantId" });
            DropIndex("GetUsGrub.RestaurantProfile", new[] { "Id" });
            DropIndex("GetUsGrub.BusinessHour", new[] { "RestaurantId" });
            DropTable("GetUsGrub.Token");
            DropTable("GetUsGrub.SecurityAnswerSalt");
            DropTable("GetUsGrub.SecurityQuestion");
            DropTable("GetUsGrub.PasswordSalt");
            DropTable("GetUsGrub.UserClaims");
            DropTable("GetUsGrub.UserAccount");
            DropTable("GetUsGrub.UserProfile");
            DropTable("GetUsGrub.RestaurantMenuItem");
            DropTable("dbo.RestaurantMenu");
            DropTable("GetUsGrub.RestaurantProfile");
            DropTable("GetUsGrub.BusinessHour");
        }
    }
}
