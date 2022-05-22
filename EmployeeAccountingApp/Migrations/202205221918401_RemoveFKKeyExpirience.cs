namespace EmployeeAccountingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFKKeyExpirience : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Experiences");
            AddColumn("dbo.Experiences", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Experiences", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Experiences");
            DropColumn("dbo.Experiences", "Id");
            AddPrimaryKey("dbo.Experiences", new[] { "EmployeeId", "LanguageId" });
        }
    }
}
