using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeAccountingApp.Models
{
    public  class User
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Логин")]
        [Required]
        [Remote("IsUserExists", "Users", ErrorMessage = "Пользователь с таким логином уже существует")]
        public string Login { get; set; }
        [DisplayName("Пароль")]
        [Required]
        public string Password { get; set; }
        [DisplayName("Время последнего действия")]
        public DateTime LastActionTime { get; set; }
    }
}