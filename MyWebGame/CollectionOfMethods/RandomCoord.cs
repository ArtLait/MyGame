using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public static class RandomCoord
    {
        public static Positions Monster(int SizeX, int SizeY)
        {
            Random rnd = new Random();
            return new Positions()
            {
                x = rnd.Next(-SizeX, SizeX),
                y = rnd.Next(-SizeY, SizeY)
            };
        }
    }
    public class Positions
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}