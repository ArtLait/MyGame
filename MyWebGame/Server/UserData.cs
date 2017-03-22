using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public class UserData
    {
        public string ConnectionId { get; set; }    
        public float PosX { get; set; }
        public float PosY { get; set; }
        public double Rotation { get; set; }
        public double SizeX { get; set; }
        public double SizeY { get; set; }
    }    
}