using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeAccountingApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Имя")]
        [Required]
        public string Name { get; set;}
        [DisplayName("Фамилия")]
        [Required]
        public string Surname { get; set; }
        [DisplayName("Возраст")]
        [Range(16,120)]
        public short Age { get; set; }
        [DisplayName("Пол")]
        [Required]
        public string Gender { get; set; }
        [DisplayName("Отдел")]
        [ForeignKey("DepartamentId")]
        public virtual Department Department { get; set; }
        public int DepartamentId { get; set; }

        public bool IsDeleted { get; set; }
    }
}