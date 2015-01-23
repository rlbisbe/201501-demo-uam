using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.DAL
{
    public class TaskContext : DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }

        public TaskContext()
            : base("TaskConnectionString")
        {

        }
    }
}