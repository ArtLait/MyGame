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
    public class World : ChatHub, ITickable
    {
        public double x = 0;
        public double y = 0;
        public double z = 0;             

        public void AddClient(dynamic client)
        {            
          //  WorldClients.Add(client);
        }
        public void Ticked(uint TickTime)
        {            
            // List<UserForChat> Users = ChatHub.Users;
           //  WorldClients.Caller.testConsoleLog("Ticked");
        }
        public void testServer(string TestMessage)
        {
             Clients.All.testClient(TestMessage);
        }       
    }
    
    public class ChatHub : Hub, ITickable
    {        
        UserRepository repo;
        public static List<UserForChat> Users = new List<UserForChat>();
        public static World world = new World();
        public static ChatHub hub = new ChatHub();          
        public ChatHub()
        {                      
            repo = new UserRepository();           
        }
        public void Ticked(uint ms, object Clients)
        {               
            var WorldClients = (IHubCallerConnectionContext<dynamic>)Clients;
            WorldClients.All.testConsoleLog("Ticked");
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
        public void eventHandlerTestArtem()
        {
            string result = "- it is work";
            Clients.All.eventHundler(result);
            //   Position position = world.Mooved(right);            
        }
        // Подключение нового пользователя  
        public void Connect(string userName)
        {                           
            
         //   world.Ticked(20); 
         //   eventHandlerTestArtem();
        //    Ticked(20);
       //   world.testServer("successful");
            
            string id = Context.ConnectionId;

            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new UserForChat { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                Clients.Caller.TakeUserName(userName);                

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
                world.AddClient(Clients.Client(id));
                var idThread = Server.Server.CreateStream(hub, Clients);
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