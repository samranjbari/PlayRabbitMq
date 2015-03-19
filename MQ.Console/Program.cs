using System;
using System.Threading;
using Funq;
using Microsoft.AspNet.SignalR.Client;
using MQ.Web.Models;
using MQ.Web.Services;
using ServiceStack;
using ServiceStack.RabbitMq;

namespace MQ.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Console.SetOut(OutWriter.FileWriter());

            var serverApp = new ServerAppHost();
            serverApp.Init();
            //serverApp.Start("http://localhost:81/");

            //var rounds = 0;
            //while (true)
            //{
            //    var input = System.Console.ReadLine();
            //    //var msg = "round {0}".Fmt(rounds);
            //    System.Console.WriteLine("SENDING: {0}", input);
            //    serverApp.SendMessage(input);
            //}


            ListenForQueue();
            System.Console.ReadLine();
        }

        private static void ListenForQueue()
        {
            var mqServer = new RabbitMqServer("10380D-DEEGAN:5672");
            mqServer.RegisterHandler<Person>(m =>
            {
                Person request = m.GetBody();

                // pretend to do some work
                //Thread.Sleep(2000);

                //var url = "http://localhost:50398/";
                //var connection = new HubConnection(url);
                //var _hubProxy = connection.CreateHubProxy("MessageHub");
                //connection.Start().Wait();
                //_hubProxy.Invoke("Recieve",string.Format("{0} - {1}",  request.Name, request.SentTime.ToString("MM/dd hh:ss"))).Wait();

                System.Console.WriteLine("SENDING " + request.Name);

                return null;
            });
            
            mqServer.Start();
        }
    }

    public class ServerAppHost : AppHostHttpListenerBase
    {
        private RabbitMqServer mqServer;
        //private RedisMqServer mqServer;

        public ServerAppHost()
            : base("App Server", typeof(PersonService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            //var redisFactory = new PooledRedisClientManager("10380D-DEEGAN:6379");
            //container.Register<IRedisClientsManager>(redisFactory);
            //mqServer = new RedisMqServer(redisFactory, retryCount: 2);
            mqServer = new RabbitMqServer("10380D-DEEGAN:5672");
            
            mqServer.Start();
        }

        public void SendMessage(string msg)
        {
            using (var mqClient = mqServer.CreateMessageQueueClient())
            {
                mqClient.Publish(new Person
                {
                    Name = msg,
                    Age = 1,
                    DateOfBirth = new DateTime(2015, 2, 9)
                });
            }
        }
    }
}