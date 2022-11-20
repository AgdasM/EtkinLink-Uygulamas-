using Bitirme_Projesi.Models;
using EtkinLink;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme_Projesi.Controllers
{
    public class AdminController : Controller
    {
        EtkinLinkContext context = new EtkinLinkContext();
        
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminPanel(AdminModel admin)
        {
            var admininDb = context.AuthorizationTbls.FirstOrDefault(x => x.Password==admin.Password && x.Gmail==admin.Mail);

            if (admininDb!=null)
            {
                return RedirectToAction("AdminPanelMenagement2", "Admin");
            }
            else
            {
                ViewBag.Message = " Geçersiz Yönetici. Admin mail veya şifre hatalı";
                return View();
            }

        }
        [HttpGet]
        public IActionResult AdminPanelMenagement()
        {
            List<CityViewModel> model = context.CityTbls.Select(x =>new CityViewModel() { City =x.City}).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult AdminPanelMenagement(string City)
        {
            CityTbl aa = new CityTbl();
            aa.City=City;
            context.CityTbls.Add(aa);
            context.SaveChanges();
            List<CityViewModel> model = context.CityTbls.Select(x => new CityViewModel() { City = x.City }).ToList();
            return View(model);
        }
        public IActionResult AdminPanelMenagement2()
        {

            List<ActivitiesViewModel> model = context.ActivityTbls.Where(z=>z.Approved == null).Select(x => new ActivitiesViewModel()
            {
                ActivityID=x.ActivityId,
                ActivityName=x.ActivityName,
                ActivityDate=x.ActivityDate.ToString(),
                ActivityDeadLine=x.ActivityDeadLine.ToString(),
                Explanation=x.Explanation,
                City=x.City.City,
                Address=x.Address,
                Quota=x.Quota,
                Ticket=x.Ticket.ToString(),
                Category=x.Category.CategoryName,
                TicketPrice=x.TicketPrice
                
            }).ToList();
            return View(model);
        }
        public IActionResult ActivityApprove(int id)
        {
            var model = context.ActivityTbls.SingleOrDefault(x => x.ActivityId==id);
            model.Approved = true;
            context.ActivityTbls.Update(model);

            
            context.SaveChanges();
            return RedirectToAction("AdminPanelMenagement2", "Admin");
        }
        [HttpGet]
        public IActionResult ActivityDeleteMessage(int id)
        {

            var model = context.ActivityTbls.SingleOrDefault(x => x.ActivityId==id);
            context.Remove(model);
            context.SaveChanges();
            return View();
        }
        [HttpPost]
        public IActionResult ActivityDelete(DeleteMessageViewModel delete)
        {

            DeleteMessageTbl dd = new DeleteMessageTbl();
            dd.ActivityId=delete.ActivityId;
            dd.Message=delete.Message;
            context.DeleteMessageTbls.Add(dd);
            context.SaveChanges();
            return RedirectToAction("AdminPanelMenagement2", "Admin");
        }

        
        public IActionResult ActivityDelete(int id)
        {
            var aa = context.ActivityTbls.SingleOrDefault(x => x.ActivityId==id);
            
            aa.Approved = false;
            context.ActivityTbls.Update(aa);
            DeleteMessageViewModel dd = new DeleteMessageViewModel();
            dd.ActivityId = aa.ActivityId;
            
            context.SaveChanges();
            return View(dd);
            
        }
        [HttpGet]
        public IActionResult AdminPanelMenagement3()
        {
            List<CategoryViewModel> model = context.CategoryTbls.Select(x => new CategoryViewModel() { Category =x.CategoryName }).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult AdminPanelMenagement3(string Category)
        {
            CategoryTbl aa = new CategoryTbl();
            aa.CategoryName=Category;
            context.CategoryTbls.Add(aa);
            context.SaveChanges();
            List<CategoryViewModel> model = context.CategoryTbls.Select(x => new CategoryViewModel() { Category =x.CategoryName }).ToList();
            return View(model);
        }
    }
}
