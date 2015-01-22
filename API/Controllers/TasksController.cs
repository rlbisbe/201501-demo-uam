using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class TasksController : ApiController
    {
        // GET api/values
        public IEnumerable<TaskModel> Get()
        {
            return new []{ 
                    new TaskModel { Title = "Ejemplo 1" },
                    new TaskModel { Title= "Ejemplo 2" } };
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]TaskModel value)
        {
            if (string.IsNullOrEmpty(value.Title))
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.PreconditionFailed);
                message.Content = new StringContent("Title is mandatory");
                throw new HttpResponseException(message);
            }

            return Created("/", value);
        }
    }
}
