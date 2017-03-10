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
            public ITickable Hub { get; private set; }
            public World World { get; private set; }
            public bool Run { get; set; }
            public object TestClients { get; set; }
            
            public ThreadContainer(Thread thread, ITickable hub, World world, bool run , object Clients )
            {
                TickThread = thread;
                Hub = hub;
                World = world;
                Run = run;
                TestClients = Clients;
            }           
        }
        public static int CreateStream(ITickable obj, World world, IHubCallerConnectionContext<dynamic> Clients)
        {            
            Thread thread = new Thread(new ParameterizedThreadStart(Tick));
            var threadId = thread.ManagedThreadId; //id потока
            var container = new ThreadContainer(thread, obj, world, true, Clients);
           // lock (IdOfThreads)
           //{                
           //     IdOfThreads.TryAdd(threadId, container);//добавил в словарь
           // }
                thread.Start(container);
            // создал поток для объекта
            return threadId;
        }

        private static void Tick(object obj)
        {
            var data = (ThreadContainer)obj;
            
            var myStopwatch = new System.Diagnostics.Stopwatch();  
            while (data.Run){  
               
                    long timeEnd;
                    
                    myStopwatch.Start();

                 //   ChatHub chatHub = new ChatHub();
                    data.Hub.Ticked(TickTime, data.TestClients, data.World);
                    
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