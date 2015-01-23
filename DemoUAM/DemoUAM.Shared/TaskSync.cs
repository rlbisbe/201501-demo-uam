using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DemoUAM
{
    public class TaskSync
    {
        private string url = "http://uam-demo.azurewebsites.net/api/tasks";

        public async void UploadTask(TaskModel task)
        {
            HttpClient client = new HttpClient();
            var data = JsonConvert.SerializeObject(task);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<TaskModel>> GetTasks()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            String jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TaskModel>>(jsonResponse);
        }
    }
}
