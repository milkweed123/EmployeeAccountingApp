namespace EmployeeAccountingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "IsDeleted");
        }
    }
}
