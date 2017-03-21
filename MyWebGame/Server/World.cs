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
        public Ceil[,] ArrayGrid { get; set; }
        public World()
        {
            SizeX = 2000;
            SizeY = 1000;
            SizeCellX = 200;
            SizeCellY = 100;            
            CountOfFood = 20;
            Players = new Dictionary<string, UserSession>();
            SomeFood = new List<Food>();
            Grid = new List<Ceil>();
            ArrayGrid = new Ceil[(int)(SizeX / SizeCellX), (int)(SizeY / SizeCellY)];
//            SomeFood.Add(new Food() { PosX = 0, PosY = 0, Size = 1000, Weight = 1, Color = "red" });
            CreateGrid();
            SetSomeFood(); 
            FillingGridWithFood();                            
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
            float xStep = 0;
            float yStep = 0;
            for (float i = 0; i < SizeX / SizeCellX; i ++ )
            {
                for (float j = 0; j < SizeY / SizeCellY ; j ++ )
                {
                    xStep += SizeCellX;
                    yStep += SizeCellY;
                    ArrayGrid[(int)i,(int)j] = new Ceil() { XCell = xStep, YCell = yStep, I = i, J = j };
                //    Grid.Add(new Ceil() { XCell = i, YCell = j, I = i, J = j });
                }
            }
        }
        public void FillingGridWithFood()
        {            
            foreach(var item in SomeFood)
            {
              item.IFood = Math.Floor(((item.PosX + SizeX / 2) / SizeCellX));
              item.JFood = Math.Floor(((item.PosY + SizeY / 2) / SizeCellY));
                try
                {
                    ArrayGrid[(int)item.IFood, (int)item.JFood].FoodInCeil.Add(item);
                    var a = 0;
                }
                catch(Exception e)
                {
                    var b = 0;
                }
              
           //   Grid.FirstOrDefault(t => t.I == item.IFood && t.J == item.JFood).FoodInCeil.Add(item);
            }
        }
        public void AddPlayerInArrayGrid(UserSession player)
        {
            player.Monster.I = (int)Math.Round((player.Monster.PosX + SizeX / 2) / SizeCellX);
            player.Monster.J = (int)Math.Round((player.Monster.PosY + SizeY / 2) / SizeCellY);
            ArrayGrid[player.Monster.I, player.Monster.J].PlayersInCeil.Add(player);
        }
        public void TestCollision(UserSession player)
        {
           foreach(UserSession item in ArrayGrid[player.Monster.I, player.Monster.J].PlayersInCeil.ToList())
           {
               if (TestCollisionWithOtherPlayers(player, item))
               {
                   
               }
           }
           foreach (Food item in ArrayGrid[player.Monster.I, player.Monster.J].FoodInCeil.ToList())
           {
               if (TestCollisionWithFood(player, item))
               {

               }
           }
        }
        public bool TestCollisionWithOtherPlayers(UserSession ourPlayer, UserSession otherPlayer)
        {            
            if (ourPlayer.ConnectionId == otherPlayer.ConnectionId)
            {
                return false;
            }
            if (ourPlayer.Monster.PosX - ourPlayer.Monster.SizeX < otherPlayer.Monster.PosX
                && otherPlayer.Monster.PosX < ourPlayer.Monster.PosX + ourPlayer.Monster.SizeX
               &&ourPlayer.Monster.PosY - ourPlayer.Monster.SizeY < otherPlayer.Monster.PosY
               && otherPlayer.Monster.PosY < ourPlayer.Monster.PosY + ourPlayer.Monster.SizeY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TestCollisionWithFood(UserSession ourPlayer, Food food)
        {
            if (ourPlayer.Monster.PosX - ourPlayer.Monster.SizeX < food.PosX
              && food.PosX < ourPlayer.Monster.PosX + ourPlayer.Monster.SizeX
             && ourPlayer.Monster.PosY - ourPlayer.Monster.SizeY < food.PosY
             && food.PosY < ourPlayer.Monster.PosY + ourPlayer.Monster.SizeY)
            {
                return true;
            }
            else
            {
                return false;
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
                
                AddPlayerInArrayGrid(CurrentClient);
                TestCollision(CurrentClient);
                CurrentClient.Client.initialSettings(SizeX, SizeY, someFood, CurrentClient.ConnectionId, users);

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