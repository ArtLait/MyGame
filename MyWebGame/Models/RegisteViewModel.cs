using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class RegisteViewModel
    {
        [StringLength(20, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan1AndLessThan20")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Web))]
        public string Name { get; set; }
        
        [StringLength(24, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan5AndLessThan24")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Web))]
        public string Password { get; set; }
        [StringLength(24, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan5AndLessThan24")]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "DifferentPasswords")]
        [Display(Name = "RepeatPassword", ResourceType = typeof(Resources.Web))]
        public string RepeatPassword { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "MailAddressIsIncorrect")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Web))]
        public string Email { get; set; }
    }
}