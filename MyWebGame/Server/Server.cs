using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using MyWebGam.Models;
using MyWebGam.Hubs;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Concurrent;

namespace MyWebGam.Server
{
    public static class Server
    {
        private static ConcurrentDictionary<int, ThreadContainer> IdOfThreads;
        const int TickTime = 20;
        static object locker = new object();

        public class ThreadContainer
        {
            public Thread TickThread { get; private set; }
            public ITickable World { get; private set; }
            public bool Run { get; set; }            
            public IHubCallerConnectionContext<dynamic> Clients { get; set; }
            
            public ThreadContainer(Thread thread, ITickable world, bool run)
            {
                TickThread = thread;
                World = world;
                Run = run;
           //     Clients = Clients;
            }           
        }
        public static int CreateStream(ITickable obj, IHubCallerConnectionContext<dynamic> Clients)
        {            
            Thread thread = new Thread(new ParameterizedThreadStart(Tick));
            var threadId = thread.ManagedThreadId; //id потока

           // lock (IdOfThreads)
           //{                
    //            IdOfThreads.TryAdd(threadId, new ThreadContainer(thread, obj, true));//добавил в словарь
           // }
                thread.Start(Clients);
            // создал поток для объекта
            return threadId;
        }

        private static void Tick(object obj)
        {
          //  var data = (ThreadContainer)obj;
            //var data = (ITickable)obj;
            //var data = ()obj;           
            var myStopwatch = new System.Diagnostics.Stopwatch();
            ChatHub chatHub = Hubs.ChatHub.hub;
        //  ChatHub world = Hubs.ChatHub.world;
            //добавить условие прекращения
            //while (data.Run)  
                for (var i = 0; i < 300; i++)
                {
                    long timeEnd;
                    
                    myStopwatch.Start();

                 //   ChatHub chatHub = new ChatHub();
                    chatHub.Ticked(TickTime, obj);
                    
                   //data.Ticked(TickTime);                    
                    
                    timeEnd = myStopwatch.ElapsedMilliseconds;

                    if (timeEnd < TickTime)
                        Thread.Sleep((int)(TickTime - timeEnd));

                    myStopwatch.Reset();
                }            
        }

        public static void CloseStream(int id)
        {
            lock (IdOfThreads)
            {
                var threadContainer = IdOfThreads[id];
                threadContainer.Run = false;
                IdOfThreads.TryRemove(id, out threadContainer);
            }
        }
    }
}