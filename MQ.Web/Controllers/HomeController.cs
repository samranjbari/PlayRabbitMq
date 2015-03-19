using System;
using System.Web.Mvc;
using MQ.Web.Models;
using ServiceStack;
using ServiceStack.RabbitMq;

namespace MQ.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult dowork(string msg)
        {
            //var factory = new ConnectionFactory() { HostName = "10380D-DEEGAN" };
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        var body = Encoding.UTF8.GetBytes(msg);
            //        channel.BasicPublish("amq.fanout", "", null, body);
            //    }
            //}
            

            // queue the work
            using (var mqServer = new RabbitMqServer("10380D-DEEGAN:5672"))
            {
                using (var mqClient = mqServer.CreateMessageQueueClient())
                {
                    for (int i = 0; i < 100; i++)
                    {
                        mqClient.Publish(new Person
                        {
                            Name = msg,
                            Age = 1,
                            DateOfBirth = new DateTime(2015, 2, 9),
                            SentTime = DateTime.Now
                        });
                    }
                }
            }

            return Json("done", JsonRequestBehavior.AllowGet);
        }
    }
}