using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClientApp.Models;
using Microsoft.AspNetCore.Http;
using Models;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;

        public HomeController(ILogger<HomeController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }



        public IActionResult Index(Photo photoModel)
        {
            photoModel.fileName = "sara.png"; // User Input based on Id
            Photo modelList = _apiService.GetImage(photoModel); // Call API and its path folder

            Byte[] b;
            b = System.IO.File.ReadAllBytes(modelList.filePath);
            modelList.ImageUrl = "data:image/"+ modelList .fileExt+ ";base64," + Convert.ToBase64String(b, 0, b.Length); ;

            return View(modelList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
