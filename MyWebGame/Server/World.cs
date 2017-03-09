using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebGam.Models;

namespace MyWebGam.Server
{    
   
    public class Player
    {        
        public void createPlayer()
        {

        }
        public void deletePlayer()
        {

        }
    }
    public  class Position
    {
        public double x{get; set;}
        public  double y { get; set; }
        public double z { get; set; }
    }
 
    public class Control
    {
        public string up { get; set; }
        public string down { get; set; }
        public string left { get; set; }
        public string right { get; set; }
        public void upPress(string key)
        {

        }
        public void downPress(string key)
        {

        }
        public void leftPress(string key)
        {

        }
        public void rightPress(string key)
        {

        }
        public void upDown(string key)
        {

        }
        public void downDown(string key)
        {

        }
        public void leftDown(string key)
        {

        }
        public void rightDown(string key)
        {

        }
    }
}