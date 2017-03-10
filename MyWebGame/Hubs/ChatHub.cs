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
    public class ChatHub : Hub, ITickable
    {        
        UserRepository repo;
                 
        private static List<UserForChat> Users = new List<UserForChat>();
        private static World world = new World();
        private static ChatHub hub = new ChatHub();          
        public ChatHub()
        {                      
            repo = new UserRepository();           
        }
        public void Ticked(uint ms, object Clients, World world)
        {               
            var WorldClients = (IHubCallerConnectionContext<dynamic>)Clients;            
            WorldClients.All.updateWorld(world.x, world.y, world.z);
        }
        public void mooved(int keyCode)
        {
            if (keyCode == 38 || keyCode == 87)
            {
                world.y += 10;
            }
            if (keyCode == 40 || keyCode == 83)
            {
                world.y -= 10;
            }
            if (keyCode == 37 || keyCode == 65)
            {
                world.x -= 10;
            }
            if (keyCode == 39 || keyCode == 68)
            {
                world.x += 10;
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

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
               
                var idThread = Server.Server.CreateStream(hub, world, Clients);
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