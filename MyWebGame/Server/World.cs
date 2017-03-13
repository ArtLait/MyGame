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
        public void InitialCreate(dynamic Clientr)
        {            
            foreach(UserSession item in players){
            Positions newCoord = RandomCoord.Monster((int)SizeX, (int)SizeY);
            var resultCoord = JsonConvert.SerializeObject(newCoord);
            var users = JsonConvert.SerializeObject(players);
            var currentClient = (dynamic) item.Client;
            currentClient.initialCreate(SizeX, SizeY, resultCoord, users);
            }
        }
        public void RemovePlayer(UserSession session)
        {
            players.Remove(session);
        }
    }
}