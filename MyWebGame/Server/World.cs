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
            SizeX = 80;
            SizeY = 80;        
        }
        public void Ticked(float ms)
        {
            foreach (var player in players)
            {
                player.Monster.Ticked(ms);
            }            

            foreach (var player in players)
            {
                var resultObject = players.Select(t => new UserData(){ConnectionId = t.ConnectionId, PosX = t.Monster.PosX, PosY = t.Monster.PosY});
                var result = JsonConvert.SerializeObject(resultObject);                
                player.SetPositions(result);
            }
        }      
        public void AddPlayer(UserSession session)
        {
            players.Add(session);
        }
        public void InitialCreate(string id)
        {
            var users = JsonConvert.SerializeObject(players);           
            UserSession CurrentClient = players.FirstOrDefault(t => t.ConnectionId == id);
            Positions newCoord = RandomCoord.Monster((int)SizeX, (int)SizeY);
            CurrentClient.Monster.PosX = newCoord.x;
            CurrentClient.Monster.PosY = newCoord.y;
            //CurrentClient.Client.initialSettings(SizeX, SizeY);
            
            foreach(var item in players){   
                                                         
                item.Client.addMoreMembers(SizeX, SizeY, users);
            }
        }
        public void RemovePlayer(UserSession session)
        {
            players.Remove(session);
        }
    }
}