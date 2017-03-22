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
       
        private static World world = null;
        private static int idThread = 0;
        static ChatHub()
        {
            world = new World();
            idThread = Server.Server.CreateStream(world);
        }

        public ChatHub()
        {
            repo = new UserRepository();
        }
        public void moveAndRotate(int MousePosX, int MousePosY)
        {
            world.MoveAndRotate(Context.ConnectionId, MousePosX, MousePosY);
        }
        public void moovedDown(int keycode)
        {            
            world.MooveDown(Context.ConnectionId, keycode);
        }
        public void moovedUp(int keycode)
        {
            world.MooveUp(Context.ConnectionId, keycode);
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
                // chat
                Users.Add(new UserForChat { ConnectionId = id, Name = userName });
                Clients.Caller.onConnected(id, userName, Users);
                Clients.Caller.TakeUserName(userName);
                Clients.AllExcept(id).onNewUserConnected(id, userName);

                // game
                world.AddPlayer(new UserSession(Clients.Caller, userName, Context.ConnectionId));
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
                var deletedUser = world.Players[id];
                world.RemovePlayer(deletedUser);
                Clients.All.onUserDisconnected(id, item.Name, Users);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}