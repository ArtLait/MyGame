using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Models
{
    public class UserForChat
    {         
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
}