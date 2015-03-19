using Funq;
using MQ.Web.Models;
using MQ.Web.Services;
using ServiceStack;
using ServiceStack.RabbitMq;

namespace MQ.Web.App_Start
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Person", typeof(Person).Assembly)
        {
        }

        public override void Configure(Funq.Container container)
        {
            SetConfig(new HostConfig
            {
                 HandlerFactoryPath = "api"
                 //DefaultContentType = ContentType.Json
            });

            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

            //ConfigureRabbitMqServer(container);
        }

        private void ConfigureRabbitMqServer(Container container)
        {
            //var redisFactory = new PooledRedisClientManager("10380D-DEEGAN:6379");
            //container.Register<IRedisClientsManager>(redisFactory);
            //mqServer = new RedisMqServer(redisFactory, retryCount: 2);
            var mqServer = new RabbitMqServer("10380D-DEEGAN:5672");

            mqServer.RegisterHandler<Person>(m =>
            {
                Person request = m.GetBody();

                MessageListener.Instance.TriggerMe(request.Name);

                return null;
            });


            mqServer.Start();
        }

    }
}