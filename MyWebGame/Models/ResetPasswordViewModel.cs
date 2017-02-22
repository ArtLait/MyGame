using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyWebGam.Models
{
    public class ResetPasswordViewModel
    {        
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan5")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Web))]
        public string Password { get; set; }
        [StringLength(50, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan5")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "DifferentPasswords")]
        [Display(Name = "RepeatPassword", ResourceType = typeof(Resources.Web))]
        public string RepeatPassword { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Key{ get; set; }
    }
}