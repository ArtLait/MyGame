using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class NickNameViewModel
    {
        [StringLength(255, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan1")]
        [Display(Name = "NickName")]
        public string NickName { get; set; }
    }
}