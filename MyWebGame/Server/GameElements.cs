using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebGam.Models;
using Microsoft.AspNet.SignalR;

namespace MyWebGam.Server
{
    public class World
    {
        public double x = 0;
        public double y = 0;
        public double z = 0;
    }
    public class CLientGames
    {
        public void initialCreate(List<UserForChat> Users)
        {
            Users.Last().x = 10;
            Users.Last().y = 10;
            Users.Last().z = 10;
        }
    }
    
}