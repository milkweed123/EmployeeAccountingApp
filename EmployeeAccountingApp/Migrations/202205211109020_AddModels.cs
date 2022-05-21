namespace EmployeeAccountingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        Floor = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Age = c.Short(nullable: false),
                        Gender = c.String(),
                        DepartamentId = c.Int(nullable: false),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LanguageEmployees",
                c => new
                    {
                        Language_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.Employee_Id })
                .ForeignKey("dbo.Languages", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LanguageEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.LanguageEmployees", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropIndex("dbo.LanguageEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.LanguageEmployees", new[] { "Language_Id" });
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            DropTable("dbo.LanguageEmployees");
            DropTable("dbo.Languages");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
