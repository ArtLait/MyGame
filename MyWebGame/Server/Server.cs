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
        private static ConcurrentDictionary<int, ThreadContainer> IdOfThreads = new ConcurrentDictionary<int,ThreadContainer>();
        const float TickTime = 20;

        public class ThreadContainer
        {
            public Thread TickThread { get; private set; }
            public ITickable Tickable { get; private set; }
            public bool Run { get; set; }
            
            public ThreadContainer(Thread thread, ITickable world, bool run)
            {
                TickThread = thread;
                Tickable = world;
                Run = run;
            }           
        }
        public static int CreateStream(ITickable world)
        {               
            Thread thread = new Thread(new ParameterizedThreadStart(Tick));
            var threadId = thread.ManagedThreadId; 
            var container = new ThreadContainer(thread, world, true);
            IdOfThreads.TryAdd(threadId, container);//добавил в словарь         
            thread.Start(container);            
            return threadId;
        }

        private static void Tick(object obj)
        {
            var data = (ThreadContainer)obj;
            var myStopwatch = new System.Diagnostics.Stopwatch();
            float timeEnd;

            while (data.Run)
  //              for (var i = 0; i < 5; i++)
              {
                    myStopwatch.Start();

                    data.Tickable.Ticked(TickTime);
                    timeEnd = myStopwatch.ElapsedMilliseconds;

                    if (timeEnd < TickTime)
                        Thread.Sleep((int)(TickTime - timeEnd));

                    myStopwatch.Reset();
                }            
        }

        public static void CloseStream(int id)
        {
            var threadContainer = IdOfThreads[id];
            threadContainer.Run = false;
            IdOfThreads.TryRemove(id, out threadContainer);
        }
    }
}