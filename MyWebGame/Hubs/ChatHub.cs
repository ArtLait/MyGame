using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MyWebGam.Models;
using MyWebGam.EF;
using MyWebGam.Server;
using Microsoft.AspNet.SignalR.Hubs;

namespace MyWebGam.Hubs
{
    public class ChatHub : Hub
    {
        UserRepository repo;

        private static List<UserForChat> Users = new List<UserForChat>();
        private static World world = new World();
        public ChatHub()
        {
            repo = new UserRepository();
        }
        public void moovedDown(int keycode)
        {
            string id = Context.ConnectionId;
            if (keycode == 38 || keycode == 87)
            {                
                world.players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedY = 10;
            }
            if (keycode == 40 || keycode == 83)
            {
                world.players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedY = -10;
            }
            if (keycode == 37 || keycode == 65)
            {
                world.players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedX = -10;
            }
            if (keycode == 39 || keycode == 68)
            {
                world.players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedX = 10;
            }
        }
        public void moovedUp(int keycode)
        {
            string id = Context.ConnectionId;
            if (keycode == 38 || keycode == 87)
            {
                world.players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedY = 0;
            }
            if (keycode == 40 || keycode == 83)
            {
                world.players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedY = 0;
            }
            if (keycode == 37 || keycode == 65)
            {
                world.players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedX = 0;
            }
            if (keycode == 39 || keycode == 68)
            {
                world.players.FirstOrDefault(t => t.ConnectionId == id).Monster.SpeedX = 0;
            }
        }
        public void checkAuth()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var name = repo.GetUserWithEmail(Context.User.Identity.Name);
                if (name != null)
                {
                    Connect(name);
                }
            }
        }
        // Подключение нового пользователя  
        public void Connect(string userName)
        {
            string id = Context.ConnectionId;

            if (!Users.Any(x => x.ConnectionId == id))
            {

                Users.Add(new UserForChat { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                Clients.Caller.TakeUserName(userName);
                //UserForChat clientTest = new UserForChat(Clients.Client(id));
                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
                //  UserSession testSession = new UserSession(Clients.Caller, "Artem", "555");
                //  testSession.SetPositions("Test message!");                
                world.AddPlayer(new UserSession(Clients.Caller, userName, Context.ConnectionId));
                world.InitialCreate(id);
                //   world.players.FirstOrDefault().SetPositions("Problem is resolved");
                //  TestClients testClient = new TestClients(Clients.Caller);
                //  testClient.SetPositions("Test is successful");
                // Clients.Caller.setPositions("Test is successful");
                var idThread = Server.Server.CreateStream(world);
            }
        }
        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name, Users);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}