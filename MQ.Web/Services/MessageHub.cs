using System;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MQ.Web.Models;

namespace MQ.Web.Services
{
    public class MessageHub : Hub
    {
        public void Recieve(string msg)
        {
            Clients.All.doSomething(msg);
        }
    }

    public class MessageListener
    {
        // Singleton instance
        private readonly static Lazy<MessageListener> _instance = new Lazy<MessageListener>(
            () => new MessageListener(GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients));

        private readonly ConcurrentDictionary<string, Person> _messages = 
            new ConcurrentDictionary<string, Person>();

        //private volatile MarketState _marketState;

        public MessageListener(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static MessageListener Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public void TriggerMe(string msg)
        {
            Clients.All.doSomething(msg);
        }
    }
}