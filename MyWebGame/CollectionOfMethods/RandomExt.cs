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
                "red", "green", "gray", "yellow", "#A68D00", "#FFE240","#FFEA73"
            };

        public static string GetRandomColor(int min, int max)
        {
            return _arrayColor[_rnd.Next(min, max)];
        }
        public static ResultPosition GetRandomPosition(int SizeX, int SizeY, int SizeMonsterX, int SizeMonsterY)
        {
            return new ResultPosition()
            {
                x = _rnd.Next(-SizeX / 2 + SizeMonsterX, SizeX / 2 - SizeMonsterX),
                y = _rnd.Next(-SizeY / 2 + SizeMonsterY, SizeY / 2 - SizeMonsterY)
            };
        }
        public static float GetRandomSize(int minSize, int maxSize)
        {
            return (float)_rnd.Next(minSize, maxSize);
        }
        public static int GetRandomWeight(int minWeight, int maxWeight)
        {
            return _rnd.Next(minWeight, maxWeight);
        }
    }
    public class ResultPosition
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}