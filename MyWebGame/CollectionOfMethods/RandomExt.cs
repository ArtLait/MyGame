using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public static class RandomExt
    {
        private static Random _rnd = new Random();
        private static string[] _arrayColor = new string[]{
                "red", "green", "gray"
            };

        public static string GetRandomColor()
        {
            return _arrayColor[_rnd.Next(0, 3)];
        }
         public static PositionMonster GetRandomMonster(int SizeX, int SizeY, int SizeMonsterX, int SizeMonsterY)
        {            
            return new PositionMonster()
            {
                x = _rnd.Next(-SizeX/2 + SizeMonsterX, SizeX/2 - SizeMonsterX),
                y = _rnd.Next(-SizeY/2 + SizeMonsterY, SizeY/2 - SizeMonsterY)
            };
        }
    }
    public class PositionMonster
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}