using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class AuthorizationViewModel
    {
        [StringLength(255, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan1")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Web))]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan5")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Web))]
        public string Password { get; set; }
    }
}