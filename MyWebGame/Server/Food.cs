using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public class Food
    {
        public static int MinSize = 10;
        public static int MaxSize = 31;
        public static int MinWeight = 1;
        public static int MaxWeight = 3;
        public string Id { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float Size { get; set; }
        public int Weight { get; set; }
        public string Color { get; set; }
        public int IFood { get; set; }
        public int JFood { get; set; }
        public Food()
        {            

        }
    }
}