using System;
using MQ.Web.Models;
using ServiceStack;

namespace MQ.Web.Services
{
    public class PersonService : Service
    {
        public object Any(Person request)
        {
            return new Person
            {
                Id = 73,
                Name = "J Doe",
                Age = 30,
                DateOfBirth = new DateTime(1984, 10, 8)
            };
        }
    }
}