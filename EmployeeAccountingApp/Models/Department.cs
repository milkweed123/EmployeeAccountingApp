using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeAccountingApp.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Название отдела")]
        public string Caption { get; set; }
        [DisplayName("Этаж")]
        public short Floor { get; set; }
        
    }
}