﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class PlayNowViewModel
    {
        [StringLength(20, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Web), ErrorMessageResourceName = "LengthRequiredMoreThan1AndLessThan20")]
        [Display(Name = "EntryNickName", ResourceType = typeof(Resources.Web))]
        public string Name { get; set; }        
    }
}