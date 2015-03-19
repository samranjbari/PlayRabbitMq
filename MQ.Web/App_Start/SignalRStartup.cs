using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(MQ.Web.App_Start.SignalRStartup))]
namespace MQ.Web.App_Start
{
    public class SignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}