using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ClientApp.Models
{
    public class ApiService : IApiService
    {
        private string url = "http://localhost:65123/api/Image/";
        Uri baseAddress = new Uri("http://localhost:65123/api");
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = baseAddress;
        }
        public Photo GetImage(Photo photo)
        {
            Photo modelList = new Photo();
            HttpResponseMessage response = httpClient.GetAsync(httpClient.BaseAddress + "/image?fileName="+photo.fileName+"").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<Photo>(data);
            }
            return modelList;
        }

        public HttpResponseMessage UploadImage(Photo photo)
        {
            string data = JsonConvert.SerializeObject(photo);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync(httpClient.BaseAddress + "/image", content).Result;
            return response;
        }
    }
}
