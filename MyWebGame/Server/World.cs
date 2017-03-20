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
        public float SizeCellX { get; private set; }
        public float SizeCellY { get; private set; }
        public int CountOfFood { get; private set; }
        public Dictionary<string, UserSession> Players { get; private set; }
        public List<Food> SomeFood { get; private set; }
        public List<Ceil> Grid { get; private set; }
        public World()
        {
            SizeX = 2000;
            SizeY = 1000;
            SizeCellX = 200;
            SizeCellY = 200;            
            CountOfFood = 20;
            Players = new Dictionary<string, UserSession>();
            SomeFood = new List<Food>();
            Grid = new List<Ceil>();
//            SomeFood.Add(new Food() { PosX = 0, PosY = 0, Size = 1000, Weight = 1, Color="red" });
            CreateGrid();
            FillingGridWithFood();
            SetSomeFood();            
        }      
        private void SetSomeFood()
        {
            for (var i = 0; i < CountOfFood; i++)
            {
                ResultPosition coord = RandomExt.GetRandomPosition((int)SizeX, (int)SizeY, Food.MaxSize, Food.MaxSize);
                SomeFood.Add(new Food()
                {
                    Size = RandomExt.GetRandomSize(Food.MinSize, Food.MaxSize),
                    Weight = RandomExt.GetRandomWeight(Food.MinWeight, Food.MaxWeight),
                    PosX = coord.x,
                    PosY = coord.y,
                    Color = RandomExt.GetRandomColor(3, 7)
                });
            }
        }
        public void CreateGrid()
        {
            for (float i = 0; i < SizeX; i += SizeCellX)
            {
                for (float j = 0; j < SizeY; j += SizeCellY)
                {
                    Grid.Add(new Ceil() { XCell = i, YCell = j });
                }
            }
        }
        public void FillingGridWithFood()
        {            
            foreach(Food item in SomeFood)
            {
              item.XCell = Math.Round(item.PosX / SizeCellX);
              item.YCell = Math.Round(item.PosY / SizeCellY);
              Grid.FirstOrDefault(t => t.XCell == item.XCell && t.YCell == item.YCell).FoodInCeil.Add(item);
            }
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

                var resultObject = Players.Select(t => new UserData()
                {
                    ConnectionId = t.Value.ConnectionId,
                    PosX = t.Value.Monster.PosX,
                    PosY = t.Value.Monster.PosY,
                    Rotation = t.Value.Monster.Rotation
                });
                var result = JsonConvert.SerializeObject(resultObject);

                foreach (var player in Players)
                {
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
                var someFood = JsonConvert.SerializeObject(SomeFood);
                var data = Players.Select(t => new DataForInitialCreate()
                {
                    ConnectionId = t.Key,
                    PosX = t.Value.Monster.PosX,
                    PosY = t.Value.Monster.PosY,
                    SizeX = t.Value.Monster.SizeX,
                    SizeY = t.Value.Monster.SizeY,
                    Color = t.Value.Monster.Color
                });
                var users = JsonConvert.SerializeObject(data);
                UserSession CurrentClient = session;
                ResultPosition newCoord = RandomExt.GetRandomPosition((int)SizeX, (int)SizeY, CurrentClient.Monster.SizeX, CurrentClient.Monster.SizeY);
                CurrentClient.Monster.PosX = newCoord.x;
                CurrentClient.Monster.PosY = newCoord.y;               
                CurrentClient.Client.initialSettings(SizeX, SizeY, someFood, CurrentClient.ConnectionId);

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