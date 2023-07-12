using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeTask.Service
{
    public class MonitorService
    {
        private SupportQueue supportQueue;
        private Timer timer;

        public MonitorService(SupportQueue queue)
        {
            supportQueue = queue;
        }

        public void StartMonitoring()
        {
            timer = new Timer(MonitorQueue, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public void StopMonitoring()
        {
            timer?.Dispose();
        }

        private void MonitorQueue(object state)
        {
            //var inactiveSessions = supportQueue.GetInactiveSessions();

            //foreach (var session in inactiveSessions)
            //{
            //    Console.WriteLine($"Session {session.SessionId} is inactive.");
            //}
        }
    }
}
