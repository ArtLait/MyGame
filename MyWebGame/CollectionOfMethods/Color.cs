using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public class Color
    {
        public string NewRandom()
        {
            string[] arrayColor = new string[]{
                "red", "green", "gray"
            };            
            Random rnd = new Random();
            int rndResult = rnd.Next(1, 4);
            switch (rndResult)
            {
                case 1:
                    return arrayColor[0];
                case 2:
                    return arrayColor[1];
                case 3:
                    return arrayColor[2];
                default:
                    return "black";
            }
        }
    }
}