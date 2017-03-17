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

        public Dictionary<string, UserSession> Players { get; private set; }
        public World()
        {
            SizeX = 2000;
            SizeY = 1000;
            Players = new Dictionary<string, UserSession>();
        }

        public void Ticked(float ms)
        {
            lock (Players)
            {
                foreach (var player in Players)
                {
                    if (CheckTheBorder(player.Value.Monster, ms))
                        player.Value.Monster.Ticked(ms);
                }

                foreach (var player in Players)
                {
                    var resultObject = Players.Select(t => new UserData()
                    {
                        ConnectionId = t.Value.ConnectionId,
                        PosX = t.Value.Monster.PosX,
                        PosY = t.Value.Monster.PosY,
                        Rotation = t.Value.Monster.Rotation
                    });
                    var result = JsonConvert.SerializeObject(resultObject);
                    player.Value.SetPositions(result);
                }
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
            lock (Players)
            {
                Players.Add(session.ConnectionId, session);

                var data = Players.Select(t => new DataForInitialCreate()
                {
                    PosX = t.Value.Monster.PosX,
                    PosY = t.Value.Monster.PosY,
                    SizeX = t.Value.Monster.SizeX,
                    SizeY = t.Value.Monster.SizeY,
                    Color = t.Value.Monster.Color
                });
                var users = JsonConvert.SerializeObject(data);
                UserSession CurrentClient = session;
                PositionMonster newCoord = RandomExt.GetRandomMonster((int)SizeX, (int)SizeY, CurrentClient.Monster.SizeX, CurrentClient.Monster.SizeY);
                CurrentClient.Monster.PosX = newCoord.x;
                CurrentClient.Monster.PosY = newCoord.y;
                CurrentClient.Client.initialSettings(SizeX, SizeY);

                foreach (var item in Players)
                {
                    item.Value.Client.addMoreMembers(SizeX, SizeY, users);
                }
            }
        }

        public void MoveAndRotate(string id, float dirX, float dirY /*int MousePosX, int MousePosY*/)
        {            
            if (!(dirX == 0.0f && dirY == 0.0f))
            {
                lock (Players)
                {
                    var length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                    var player = Players[id];
                    var monster = player.Monster;
                    monster.Rotation = Math.Acos(dirX / (length + 1));
                    if (dirY < 0f)
                    {
                        monster.Rotation = -monster.Rotation;
                    }
                    monster.SpeedX = monster.Speed * dirX / length;
                    monster.SpeedY = monster.Speed * dirY / length;
                }
            }
        }      
        public void MooveDown(string id, int keycode)
        {
            lock (Players)
            {            
                if (keycode == 38 || keycode == 87)
                {
                    Players[id].Monster.SpeedY = 10;
                }
                if (keycode == 40 || keycode == 83)
                {
                    Players[id].Monster.SpeedY = -10;
                }
                if (keycode == 37 || keycode == 65)
                {
                    Players[id].Monster.SpeedX = -10;
                }
                if (keycode == 39 || keycode == 68)
                {
                    Players[id].Monster.SpeedX = 10;
                }
            }
        }
        public void MooveUp(string id, int keycode)
        {
            lock (Players)
            {
                if (keycode == 38 || keycode == 87)
                {
                    Players[id].Monster.SpeedY = 0;
                }
                if (keycode == 40 || keycode == 83)
                {
                    Players[id].Monster.SpeedY = 0;
                }
                if (keycode == 37 || keycode == 65)
                {
                    Players[id].Monster.SpeedX = 0;
                }
                if (keycode == 39 || keycode == 68)
                {
                    Players[id].Monster.SpeedX = 0;
                }
            }
        }      
        public void RemovePlayer(UserSession session)
        {
            lock (Players)
            {
                Players.Remove(session.ConnectionId);
            }
        }
    }
}