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
            int rndResult = rnd.Next(0, 2);
            return arrayColor[rndResult];
        }
    }
}