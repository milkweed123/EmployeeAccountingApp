using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeAccountingApp.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("LanguageId ")]
        public virtual Language Language { get; set; }
        public int LanguageId { get; set; }
    }
}