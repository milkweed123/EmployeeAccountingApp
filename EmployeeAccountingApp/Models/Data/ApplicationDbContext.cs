using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeAccountingApp.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet <Department> Departments { get; set; }
        public DbSet <Employee> Employees { get; set; }

        public DbSet<Language> Languages { get; set; }

        public ApplicationDbContext() :base("DefaultConnection")
        {

        }
    }
}