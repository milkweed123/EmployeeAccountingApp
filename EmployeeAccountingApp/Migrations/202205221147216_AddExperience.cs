namespace EmployeeAccountingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExperience : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LanguageEmployees", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.LanguageEmployees", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.LanguageEmployees", new[] { "Language_Id" });
            DropIndex("dbo.LanguageEmployees", new[] { "Employee_Id" });
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.LanguageId })
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.LanguageId);
            
            DropTable("dbo.LanguageEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LanguageEmployees",
                c => new
                    {
                        Language_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.Employee_Id });
            
            DropForeignKey("dbo.Experiences", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Experiences", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Experiences", new[] { "LanguageId" });
            DropIndex("dbo.Experiences", new[] { "EmployeeId" });
            DropTable("dbo.Experiences");
            CreateIndex("dbo.LanguageEmployees", "Employee_Id");
            CreateIndex("dbo.LanguageEmployees", "Language_Id");
            AddForeignKey("dbo.LanguageEmployees", "Employee_Id", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LanguageEmployees", "Language_Id", "dbo.Languages", "Id", cascadeDelete: true);
        }
    }
}
