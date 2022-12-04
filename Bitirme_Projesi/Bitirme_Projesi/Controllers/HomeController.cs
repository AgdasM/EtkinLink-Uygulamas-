using Bitirme_Projesi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Bitirme_Projesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected HttpClient httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            httpClient=new HttpClient();
            httpClient.BaseAddress=new Uri("http://localhost:6821/Developer");
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DevSingUp()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DevSingUp(DevSingModel dev)
        {
            
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync("api/Developer", dev);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string token = await httpResponseMessage.Content.ReadAsStringAsync();
                ViewBag.Token = token;
                return View();
            }
            else
            {
                ViewBag.Token = "Token Gelmedi..";
                return View();
            }
           
        }
        [HttpGet]
        public async Task<IActionResult> SendEtkinlik(SendEtkinlink send)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("http://localhost:6821/api/DevGet", send);
            return View();
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