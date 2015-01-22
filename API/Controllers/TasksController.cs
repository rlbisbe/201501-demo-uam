using API.DAL;
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
        private TaskContext db = new TaskContext();
        // GET api/tasks
        public IEnumerable<TaskModel> Get()
        {
            return db.Tasks.ToList();
        }

        // POST api/tasks
        public IHttpActionResult Post([FromBody]TaskModel value)
        {
            if (string.IsNullOrEmpty(value.Title))
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.PreconditionFailed);
                message.Content = new StringContent("Title is mandatory");
                throw new HttpResponseException(message);
            }
            var result = db.Tasks.Add(value);
            db.SaveChanges();
            return Created("/", result);
        }
    }
}
