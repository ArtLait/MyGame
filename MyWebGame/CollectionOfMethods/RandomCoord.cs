using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public static class NewRandom
    {
        public static PositionMonster CoordMonster(int SizeX, int SizeY, int SizeMonsterX, int SizeMonsterY)
        {
            Random rnd = new Random();
            return new PositionMonster()
            {
                x = rnd.Next(-SizeX/2 + SizeMonsterX, SizeX/2 - SizeMonsterX),
                y = rnd.Next(-SizeY/2 + SizeMonsterY, SizeY/2 - SizeMonsterY)
            };
        }       
    }   
    public class PositionMonster
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}