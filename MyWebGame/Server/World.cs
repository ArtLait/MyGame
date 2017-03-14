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
                if(CheckTheBorder(player, ms))
                player.Monster.Ticked(ms);                
            }            

            foreach (var player in players)
            {
                var resultObject = players.Select(t => new UserData(){ConnectionId = t.ConnectionId, PosX = t.Monster.PosX, PosY = t.Monster.PosY});
                var result = JsonConvert.SerializeObject(resultObject);                
                player.SetPositions(result);
            }
        }
        private bool CheckTheBorder(UserSession player, float ms)
        {
            bool InWorldSize = true;
            var PosX = player.Monster.PosX;
            var PosY = player.Monster.PosY;
            var SizeMonsterX = player.Monster.SizeX;
            var SizeMonsterY = player.Monster.SizeY;
            var SpeedX = player.Monster.SpeedX;
            var SpeedY = player.Monster.SpeedY;
            var newPosX = ms * SpeedX / 1000;
            var newPosY = ms * SpeedY / 1000;
            if ( PosX > SizeX / 2)
            {
                player.Monster.PosX = -SizeX / 2;
                InWorldSize = false;
            }
            if (PosY > SizeY / 2)
            {
                player.Monster.PosY = -SizeY / 2;
                InWorldSize = false;
            }
            if (PosX  < -SizeX / 2)
            {
                player.Monster.PosX = SizeX / 2;
                InWorldSize = false;
            }
            if (PosY  < -SizeY / 2)
            {
                player.Monster.PosY = +SizeY / 2;
                InWorldSize = false;
            }
            return InWorldSize;
        }     
        public void AddPlayer(UserSession session)
        {
            players.Add(session);
        }
        public void InitialCreate(string id, dynamic CallerClient)
        {
            var users = JsonConvert.SerializeObject(players);           
            UserSession CurrentClient = players.FirstOrDefault(t => t.ConnectionId == id);
            Positions newCoord = NewRandom.CoordMonster((int)SizeX, (int)SizeY, CurrentClient.Monster.SizeX, CurrentClient.Monster.SizeY);
            CurrentClient.Monster.PosX = newCoord.x;
            CurrentClient.Monster.PosY = newCoord.y;
            CallerClient.initialSettings(SizeX, SizeY);
            
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