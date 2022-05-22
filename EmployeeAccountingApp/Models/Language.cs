using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeAccountingApp.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Наименование языка")]
        public string Name { get; set; }

    }
}