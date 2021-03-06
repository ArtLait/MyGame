﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebGam.Hubs;
using Microsoft.AspNet.SignalR;

namespace MyWebGam.Server
{
    public class UserSession : Hub
    {
        public dynamic Client { get; set; }
        public object TestClient { get; set; }   
        public string UserName { get; private set; }
        public string ConnectionId { get; private set; }
        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }
        public Monster Monster { get; private set; }

        public UserSession(dynamic client, string userName, string connectionId)
        {
            Client = client;
            TestClient = client;
            UserName = userName;
            ConnectionId = connectionId;
            Monster = new Monster();            
        }
        public void SetPositions(string data)
        {
            Client.setPositions(data);
        }
    }
    public class Monster : ITickable
    {
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public double Rotation { get; set; }
        public float NewPosX {get; set;}
        public float NewPosY {get; set;}
        public float NewPosZ { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public string Color { get; set; }
        public float Speed { get; private set; }

        public Monster()
        {            
            PosX = 0;
            PosY = 0;
            PosZ = 0;            
            SpeedX = 0;
            SpeedY = 0;
            SizeX = 10;
            SizeY = 20;
            Speed = 20;
            Color = RandomExt.GetRandomColor(0, 3);
        }
        public void Ticked(float ms)
        {            
            PosX += ms * SpeedX/ 1000;
            PosY += ms * SpeedY/ 1000;            
        }
    }

}