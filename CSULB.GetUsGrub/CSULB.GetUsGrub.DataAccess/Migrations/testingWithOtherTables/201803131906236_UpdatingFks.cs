namespace CSULB.GetUsGrub.DataAccess.Migrations.testingWithOtherTables
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingFks : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PasswordSalts");
            AlterColumn("dbo.PasswordSalts", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PasswordSalts", "Id");
            CreateIndex("dbo.PasswordSalts", "Id");
            AddForeignKey("dbo.PasswordSalts", "Id", "dbo.UserAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PasswordSalts", "Id", "dbo.UserAccounts");
            DropIndex("dbo.PasswordSalts", new[] { "Id" });
            DropPrimaryKey("dbo.PasswordSalts");
            AlterColumn("dbo.PasswordSalts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PasswordSalts", "Id");
        }
    }
}
