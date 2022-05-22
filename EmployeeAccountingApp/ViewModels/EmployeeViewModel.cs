using EmployeeAccountingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeAccountingApp.ViewModels
{
    public class EmployeeViewModel
    {
        public Experience Experience { get; set; }
        public Employee Employee { get; set; }
        public EmployeeViewModel()
        {
            Experience = new Experience();
            Employee = new Employee();
        }
    }
}