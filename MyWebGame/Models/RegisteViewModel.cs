using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class RegisteViewModel
    {
        [StringLength(255, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan1")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Web))]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan5")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Web))]
        public string Password { get; set; }
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan5")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "DifferentPasswords")]
        [Display(Name = "RepeatPassword", ResourceType = typeof(Resources.Web))]
        public string RepeatPassword { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "MailAddressIsIncorrect")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Web))]
        public string Email { get; set; }
    }
}