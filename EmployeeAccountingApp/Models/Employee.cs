using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeAccountingApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set;}
        public string Surname { get; set; }
        public short Age { get; set; }       
        public string Gender { get; set; }  
        public List<Language> Languages { get; set;}
        public virtual Department Department { get; set; }
        public int DepartamentId { get; set; }
    }
}