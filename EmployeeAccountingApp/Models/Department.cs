using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeAccountingApp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Caption { get; set; }
        public short Floor { get; set; }
        
    }
}