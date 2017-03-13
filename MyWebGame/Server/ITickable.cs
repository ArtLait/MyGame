using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MyWebGam.Models;
using Microsoft.AspNet.SignalR.Hubs;
using MyWebGam.Hubs;

namespace MyWebGam.Server
{
    public interface ITickable
    {
         void Ticked(uint ms);
    }
}