using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public static class NewRandom
    {
        public static Positions CoordMonster(int SizeX, int SizeY, int SizeMonsterX, int SizeMonsterY)
        {
            Random rnd = new Random();
            return new Positions()
            {
                //x = rnd.Next(-SizeX/2 + SizeMonsterX, SizeX/2 - SizeMonsterX),
                //y = rnd.Next(-SizeY/2 + SizeMonsterY, SizeY/2 - SizeMonsterY)
                x = rnd.Next(-SizeX / 2, SizeX / 2 ),
                y = rnd.Next(-SizeY / 2, SizeY / 2 )
            };
        }       
    }   
    public class Positions
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}