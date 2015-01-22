using API.DAL;
using SendGrid;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Emailer
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new TaskContext();
            var result = new StringBuilder();
            var templatePath = @"Template\MailTemplate.html";
            var finalTemplate = string.Empty;

            var toProcess = 
                (from task in db.Tasks
                where task.DueDate < DateTime.UtcNow
                select task).ToList();

            if (toProcess.Count == 0)
            {
                return;
            }
            
            Console.WriteLine("Found {0} elements to process", toProcess.Count);

            foreach (var item in toProcess)
            {
                result.Append(string.Format("<li>{0} ({1})</li>", item.Title, item.DueDate));
            }

            //Leemos la plantilla y sustituimos los elementos
            using (StreamReader reader = new StreamReader(templatePath))
            {
                var template = reader.ReadToEnd();
                finalTemplate = template.Replace("[TAREAS]", result.ToString());
            }


            //Finalmente el envío a través de sendgrid
            var username = ConfigurationManager.AppSettings["sendgrid:username"];
            var password = ConfigurationManager.AppSettings["sendgrid:password"];
            var to = ConfigurationManager.AppSettings["sendgrid:to"];
            
            SendGridMessage myMessage = new SendGridMessage();
            myMessage.AddTo(to);
            myMessage.From = new MailAddress("test@rlbisbe.net", "SendGrid Test");
            myMessage.Subject = "Tareas pendientes";
            myMessage.Html = finalTemplate;

            var credentials = new NetworkCredential(username, password);
            var transportWeb = new Web(credentials);
            transportWeb.Deliver(myMessage);
            Console.WriteLine("Mail sent");
        }
    }
}
