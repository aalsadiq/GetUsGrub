namespace CSULB.GetUsGrub.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "GetUsGrub.UserAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsFirstTimeUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "GetUsGrub.SecurityQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        QuestionType = c.String(nullable: false),
                        QuestionAnswer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserAccount", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "GetUsGrub.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ProfileName = c.String(),
                        ProfilePicture = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserAccount", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "GetUsGrub.RestaurantProfile",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ProfileName = c.String(),
                        ProfilePicture = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.UserProfile", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "GetUsGrub.RestaurantMenu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        MenuName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.RestaurantProfile", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "GetUsGrub.RestaurantMenuItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        ItemName = c.String(),
                        ItemPrice = c.Double(nullable: false),
                        ItemPicture = c.String(),
                        Tag = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("GetUsGrub.RestaurantMenu", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("GetUsGrub.PasswordSalt", "Id", "GetUsGrub.UserAccount");
            DropForeignKey("GetUsGrub.UserProfile", "Id", "GetUsGrub.UserAccount");
            DropForeignKey("GetUsGrub.RestaurantProfile", "Id", "GetUsGrub.UserProfile");
            DropForeignKey("GetUsGrub.RestaurantMenu", "RestaurantId", "GetUsGrub.RestaurantProfile");
            DropForeignKey("GetUsGrub.RestaurantMenuItem", "MenuId", "GetUsGrub.RestaurantMenu");
            DropForeignKey("GetUsGrub.Token", "Id", "GetUsGrub.UserAccount");
            DropForeignKey("GetUsGrub.SecurityQuestion", "UserId", "GetUsGrub.UserAccount");
            DropIndex("GetUsGrub.RestaurantMenuItem", new[] { "MenuId" });
            DropIndex("GetUsGrub.RestaurantMenu", new[] { "RestaurantId" });
            DropIndex("GetUsGrub.RestaurantProfile", new[] { "Id" });
            DropIndex("GetUsGrub.UserProfile", new[] { "Id" });
            DropIndex("GetUsGrub.Token", new[] { "Id" });
            DropIndex("GetUsGrub.SecurityQuestion", new[] { "UserId" });
            DropIndex("GetUsGrub.PasswordSalt", new[] { "Id" });
            DropTable("GetUsGrub.RestaurantMenuItem");
            DropTable("GetUsGrub.RestaurantMenu");
            DropTable("GetUsGrub.RestaurantProfile");
            DropTable("GetUsGrub.UserProfile");
            DropTable("GetUsGrub.Token");
            DropTable("GetUsGrub.SecurityQuestion");
            DropTable("GetUsGrub.UserAccount");
            DropTable("GetUsGrub.PasswordSalt");
        }
    }
}
