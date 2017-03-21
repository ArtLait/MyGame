using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebGam.Server
{
    public class Ceil
    {
        public double XCell { get; set; }
        public double YCell { get; set; }
        public double I { get; set; }
        public double J { get; set; }
        public List<Food> FoodInCeil { get; set; }
        public List<UserSession> PlayersInCeil { get; set; }
        public Ceil()
        {
            FoodInCeil = new List<Food>();
            PlayersInCeil = new List<UserSession>();
        }
    }
}