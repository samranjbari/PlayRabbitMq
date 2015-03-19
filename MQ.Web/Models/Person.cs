using System;
using ServiceStack;

namespace MQ.Web.Models
{
    [Route("/person")]
    public class Person
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime SentTime { get; set; }
    }
}