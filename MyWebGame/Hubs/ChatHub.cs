using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MyWebGam.Models;
using MyWebGam.EF;


namespace MyWebGam.Hubs
{
    public class ChatHub : Hub
    {
        UserRepository repo;
        static List<UserForChat> Users = new List<UserForChat>();
        public ChatHub()
        {           
            repo = new UserRepository();
        }
        public string checkAuthAndGetName()
        {
             if (Context.User.Identity.IsAuthenticated)
             {
                 var name = repo.GetUserWithEmail(Context.User.Identity.Name);
                 if (name != null)
                 {
                     Connect(name);
                     return name;
                 }
                 return "";
             }
             return "";
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

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
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
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}