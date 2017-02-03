using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MyWebGam.Models
{
    public class RegisteViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Имя должно содержать не менее 1 символов")]
        [Display(Name="Введите имя")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен включать не менее 6 символов")]
        [Display(Name="Введите Пароль")]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен включать не менее 6 символов")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Повторите пароль")]        
        public string RepeatPassword { get; set; }
        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage="Адрес почты задан неверно")]
        [Display(Name="ВВедите почту")]
        public string Email { get; set; }
    }
}