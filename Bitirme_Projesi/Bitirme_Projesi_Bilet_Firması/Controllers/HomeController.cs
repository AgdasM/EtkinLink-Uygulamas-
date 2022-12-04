using Bitirme_Projesi_Bilet_Firması.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Bitirme_Projesi_Bilet_Firması.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.GetAsync("http://localhost:6821/api/DevGet");
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                Etkinlik etkinlik = JsonSerializer.Deserialize<Etkinlik>(data);
                return View(etkinlik);
            }
            else
            {
                ViewBag.hata = "Veri Gelmedi..";
                return View();
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}