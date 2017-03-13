using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebGam.Models;
using MyWebGam.Hubs;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace MyWebGam.Server
{
    public class World : ITickable
    {       
        public float SizeX { get; private set; }
        public float SizeY { get; private set; }

        public List<UserSession> players = new List<UserSession>();
        public World()
        {
            SizeX = 800;
            SizeY = 800;
        }
        public void Ticked(uint ms)
        {
            //foreach(var player in players)
            //{                
            //    player.Value.Ticked(ms);
            //}            
           
            foreach(var player in players)
            {                
                var result = JsonConvert.SerializeObject(players);
                player.SetPositions(result);
            }
        }       
        public void AddPlayer(UserSession session)
        {
            players.Add(session);            
        }
        public void InitialCreate(dynamic Client)
        {          
            Positions newCoord = RandomCoord.Monster((int)SizeX, (int)SizeY);
            var resultCoord = JsonConvert.SerializeObject(newCoord);
            Client.initialCreate(SizeX, SizeY, resultCoord, players.LastOrDefault().UserName);
        }
        public void RemovePlayer(UserSession session)
        {
            players.Remove(session);
        }
    }   
}