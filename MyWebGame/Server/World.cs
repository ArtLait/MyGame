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
                if(CheckTheBorder(player.Monster, ms))
                player.Monster.Ticked(ms);                
            }            

            foreach (var player in players)
            {
                var resultObject = players.Select(t => new UserData(){ConnectionId = t.ConnectionId, PosX = t.Monster.PosX, PosY = t.Monster.PosY});
                var result = JsonConvert.SerializeObject(resultObject);                
                player.SetPositions(result);
            }
        }
        private bool CheckTheBorder(Monster Monster, float ms)
        {
            bool InWorldSize = true;
            var PosX = Monster.PosX;
            var PosY = Monster.PosY;
            var SizeMonsterX = Monster.SizeX;
            var SizeMonsterY = Monster.SizeY;
            var SpeedX = Monster.SpeedX;
            var SpeedY = Monster.SpeedY;
            var newPosX = ms * SpeedX / 1000;
            var newPosY = ms * SpeedY / 1000;
            if ( PosX > SizeX / 2)
            {
                Monster.PosX = -SizeX / 2;
                InWorldSize = false;
            }
            if (PosY > SizeY / 2)
            {
                Monster.PosY = -SizeY / 2;
                InWorldSize = false;
            }
            if (PosX  < -SizeX / 2)
            {
                Monster.PosX = SizeX / 2;
                InWorldSize = false;
            }
            if (PosY  < -SizeY / 2)
            {
                Monster.PosY = +SizeY / 2;
                InWorldSize = false;
            }
            return InWorldSize;
        }     
        public void AddPlayer(UserSession session)
        {
            players.Add(session);
            InitialCreate(session);
        }
        public void InitialCreate(UserSession session)
        {
            var data = players.Select(t => new DataForInitialCreate() { PosX = t.Monster.PosX, PosY = t.Monster.PosY, SizeX = t.Monster.SizeX, SizeY = t.Monster.SizeY, Color = t.Monster.Color });
            var users = JsonConvert.SerializeObject(data);
            UserSession CurrentClient = session;
            PositionMonster newCoord = RandomExt.GetRandomMonster((int)SizeX, (int)SizeY, CurrentClient.Monster.SizeX, CurrentClient.Monster.SizeY);
            CurrentClient.Monster.PosX = newCoord.x;
            CurrentClient.Monster.PosY = newCoord.y;
            CurrentClient.Client.initialSettings(SizeX, SizeY);

            foreach (var item in players)
            {

                item.Client.addMoreMembers(SizeX, SizeY, users);
            }
        }
        public void MooveDown(string id, int keycode)
        {            
            if (keycode == 38 || keycode == 87)
            {
                players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedY = 10;
            }
            if (keycode == 40 || keycode == 83)
            {
                players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedY = -10;
            }
            if (keycode == 37 || keycode == 65)
            {
                players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedX = -10;
            }
            if (keycode == 39 || keycode == 68)
            {
                players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedX = 10;
            }
        }
        public void MooveUp(string id, int keycode)
        {
            
            if (keycode == 38 || keycode == 87)
            {
               players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedY = 0;
            }
            if (keycode == 40 || keycode == 83)
            {
                players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedY = 0;
            }
            if (keycode == 37 || keycode == 65)
            {
                players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedX = 0;
            }
            if (keycode == 39 || keycode == 68)
            {
                players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedX = 0;
            }
        }      
        public void RemovePlayer(UserSession session)
        {
            players.Remove(session);
        }
    }
}