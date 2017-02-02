using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MyWebGam.Models
{
    public class UserData
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [Display(Name="Пользователь")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Пароль")]
        public string Password { get; set; }
        [Required]
        [Display(Name="Почта")]
        public string Email { get; set; }
    }
}