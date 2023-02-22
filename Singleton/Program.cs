using System;
using static System.Console;

namespace Singleton
{
    public class Program
    {
        /// <summary>
        /// Singleton Design Pattern
        /// </summary>

        public static void Main()
        {
            var funcGetInstanceAndPrintLoadBalancer = () => { 
                var lb = LoadBalancer.GetLoadBalancer();
                Console.WriteLine($"Hash LB: {lb.Hash}");
                return lb;
            };
            var loadBalancers = new List<LoadBalancer>();
            var tasks = new List<Task<LoadBalancer>>
            {
                Task<LoadBalancer>.Factory.StartNew(funcGetInstanceAndPrintLoadBalancer),
                Task<LoadBalancer>.Factory.StartNew(funcGetInstanceAndPrintLoadBalancer),
                Task<LoadBalancer>.Factory.StartNew(funcGetInstanceAndPrintLoadBalancer),
                Task<LoadBalancer>.Factory.StartNew(funcGetInstanceAndPrintLoadBalancer)
            };

            Task.WaitAll(tasks.ToArray());

            // Next, load balance 15 requests for a server
            var balancer = LoadBalancer.GetLoadBalancer();
            for (int i = 0; i < 15; i++)
            {
                string serverName = balancer.NextServer.Name;
                WriteLine($"{balancer.Hash} - Dispatch request to: " + serverName);
            }

            // Wait for user

            ReadKey();
        }
    }    

    /// <summary>
    /// The 'Singleton' class
    /// </summary>

    public sealed class LoadBalancer
    {
        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization

        private static readonly LoadBalancer instance = new LoadBalancer();
        public readonly Guid Hash = Guid.NewGuid();
        private readonly List<Server> servers;
        private readonly Random random = new Random();

        // Note: constructor is 'private'

        private LoadBalancer()
        {
            // Load list of available servers

            servers = new List<Server>
                {
                  new Server{ Name = "ServerI", IP = "120.14.220.18" },
                  new Server{ Name = "ServerII", IP = "120.14.220.19" },
                  new Server{ Name = "ServerIII", IP = "120.14.220.20" },
                  new Server{ Name = "ServerIV", IP = "120.14.220.21" },
                  new Server{ Name = "ServerV", IP = "120.14.220.22" },
                };
        }

        public static LoadBalancer GetLoadBalancer()
        {
            return instance;
        }

        // Simple, but effective load balancer

        public Server NextServer
        {
            get
            {
                int r = random.Next(servers.Count);
                return servers[r];
            }
        }
    }

    /// <summary>
    /// Represents a server machine
    /// </summary>

    public class Server
    {
        public string Name { get; set; }
        public string IP { get; set; }
    }
}

