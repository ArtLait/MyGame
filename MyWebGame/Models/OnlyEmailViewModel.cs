using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class OnlyEmailViewModel
    {
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
            ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "MailAddressIsIncorrect")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Web))]
        public string Email { get; set; }
    }
}