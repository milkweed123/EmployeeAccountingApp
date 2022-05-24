namespace EmployeeAccountingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersLastActionTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastActionTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastActionTime");
        }
    }
}
