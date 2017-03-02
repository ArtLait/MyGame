using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class AuthorizationViewModel
    {
        [StringLength(20, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan1AndLessThan20")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Web))]
        public string Name { get; set; }
        [StringLength(24, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan5AndLessThan24")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}