using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public class DataForInitialCreate
    {
        public string ConnectionId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float SizeX { get; set; }
        public float SizeY { get; set; }
        public string Color { get; set; }
    }
}