using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class AuthorizationViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Имя должно содержать не менее 2 символов")]
        [Display(Name = "Введите имя")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен включать не менее 6 символов")]
        [Display(Name = "Введите Пароль")]
        public string Password { get; set; }
    }
}