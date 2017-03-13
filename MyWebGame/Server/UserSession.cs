using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebGam.Hubs;
using Microsoft.AspNet.SignalR;

namespace MyWebGam.Server
{
    public class UserSession : Hub
    {
        public object Client { get; set; }       
        public string UserName { get; private set; }
        public string ConnectionId { get; private set; }
        public Monster Monster { get; private set; }

        public UserSession(dynamic client, string userName, string connectionId)
        {
            Client = client;      
            UserName = userName;
            ConnectionId = connectionId;
            Monster = new Monster();                       
        }
        public void SetPositions(string data)
        {
            var MyClient = (dynamic)Client;
            MyClient.setPositions(data);
        }
    }
    public class Monster : ITickable
    {
        public float SizeX { get; private set; }
        public float SizeY { get; private set; }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }

        public Monster()
        {
            PosX = 0;
            PosY = 0;
            PosZ = 0;
            SpeedX = 0;
            SpeedY = 0;
        }
        public void Ticked(uint ms)
        {
            PosX += (float)0.02 * SpeedX;
            PosY += (float)0.02 * SpeedY;
        }
    }

}