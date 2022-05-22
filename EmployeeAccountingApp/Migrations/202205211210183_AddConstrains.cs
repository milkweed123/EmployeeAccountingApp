namespace EmployeeAccountingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConstrains : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            DropColumn("dbo.Employees", "DepartamentId");
            RenameColumn(table: "dbo.Employees", name: "Department_Id", newName: "DepartamentId");
            AlterColumn("dbo.Departments", "Caption", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "DepartamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "DepartamentId");
            AddForeignKey("dbo.Employees", "DepartamentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "DepartamentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DepartamentId" });
            AlterColumn("dbo.Employees", "DepartamentId", c => c.Int());
            AlterColumn("dbo.Employees", "Gender", c => c.String());
            AlterColumn("dbo.Employees", "Surname", c => c.String());
            AlterColumn("dbo.Employees", "Name", c => c.String());
            AlterColumn("dbo.Departments", "Caption", c => c.String());
            RenameColumn(table: "dbo.Employees", name: "DepartamentId", newName: "Department_Id");
            AddColumn("dbo.Employees", "DepartamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "Department_Id");
            AddForeignKey("dbo.Employees", "Department_Id", "dbo.Departments", "Id");
        }
    }
}
