using Bitirme_Projesi.Models;
using EtkinLink;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bitirme_Projesi.Controllers
{
    public class MemberShipController : Controller
    {
        EtkinLinkContext context = new EtkinLinkContext();
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(string Mail, string Password)
        {
            var UserDB = context.UserTbls.FirstOrDefault(x => x.Password==Password && x.Mail==Mail);
            UserModel user = new UserModel();
                
            if (UserDB!=null)
            {
                user.UserID = UserDB.UserId;
                return RedirectToAction("AttendEvent", "MemberShip", new { UserID = user.UserID });
            }
            else
            {
                ViewBag.Message = " Kullanıcı mail veya şifre hatalı";
                return View();
            }

        }
        [HttpGet]
        public IActionResult UserSingin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserSingin(UserModel user)
        {

            if (user.Password==user.PasswordAgain)
            {
                var UserDb = context.UserTbls.FirstOrDefault(x => x.Mail==user.Mail);

                if (UserDb==null)
                {
                    UserTbl kayıt = new UserTbl();
                    kayıt.UserName=user.Name;
                    kayıt.UserSurname=user.Surname;
                    kayıt.Mail=user.Mail;
                    kayıt.Password=user.Password;
                    context.UserTbls.Add(kayıt);
                    context.SaveChanges();
                    return RedirectToAction("SuccesfullyRegistered", "MemberShip");
                }
                else
                {
                    ViewBag.Message = "Email address registered in the system";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Pasword and Password again is not equal..";
                return View();
            }


        }
        public IActionResult SuccesfullyRegistered()
        {
            return View();
        }

        public IActionResult AttendEvent(int UserID)
        {
            List<ActivitiesViewModel> model = context.ActivityTbls.Where(z => z.Approved == true).Select(x => new ActivitiesViewModel()
            {
                UserID=UserID,
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
        public IActionResult JoinEvent(int activityid, int userid)
        {
            ParticipantsTbl pp = new ParticipantsTbl();
            pp.ActivityId=activityid;
            pp.UserId=userid;
            context.ParticipantsTbls.Add(pp);
            context.SaveChanges();
            return RedirectToAction("ShowJoinEvent", "MemberShip", new { userid = userid });
        }
        public IActionResult ShowJoinEvent(int userid)
        {

            IQueryable aa = from A in context.ActivityTbls
                            join P in context.ParticipantsTbls on A.ActivityId equals P.ActivityId
                            where userid==P.UserId
                            select new ActivitiesViewModel()
                            {
                                UserID=P.UserId,
                                ActivityID=A.ActivityId,
                                ActivityName=A.ActivityName,
                                ActivityDate=A.ActivityDate.ToString(),
                                ActivityDeadLine=A.ActivityDeadLine.ToString(),
                                Explanation=A.Explanation,
                                City=A.City.City,
                                Address=A.Address,
                                Quota=A.Quota,
                                Ticket=A.Ticket.ToString(),
                                Category=A.Category.CategoryName,
                                TicketPrice=A.TicketPrice
                            };

            return View(aa);
        }
        [HttpGet]
        public IActionResult CreateEvent()
        {

            CreateEventViewModels a = new CreateEventViewModels();
            List<CityViewModel> modelcity = context.CityTbls.Select(x => new CityViewModel() { City =x.City, CityID=x.CityId }).ToList();
            List<CategoryViewModel> modelcategory = context.CategoryTbls.Select(x => new CategoryViewModel() { Category =x.CategoryName, CategoryID=x.CategoryId }).ToList();

            a.City=modelcity;
            a.Category=modelcategory;


            return View(a);
        }
        [HttpPost]
        public IActionResult CreateEvent(ActivitiesViewModel act)
        {
            ActivityTbl aa = new ActivityTbl();



            aa.ActivityName = act.ActivityName;
            aa.ActivityDate=Convert.ToDateTime(act.ActivityDate);
            aa.ActivityDeadLine=Convert.ToDateTime(act.ActivityDeadLine);
            aa.Explanation=act.Explanation;
            aa.CityId=Convert.ToInt32(act.City);
            aa.Address=act.Address;
            aa.Quota=act.Quota;
            aa.Ticket=Convert.ToBoolean(act.Ticket);
            aa.CategoryId=Convert.ToInt32(act.Category);
            aa.TicketPrice=act.TicketPrice;

            context.ActivityTbls.Add(aa);

            context.SaveChanges();

            //var admininDb = context.AuthorizationTbls.FirstOrDefault(x => x.Password==admin.Password && x.Gmail==admin.Mail);

            //if (admininDb!=null)
            //{
            //    return RedirectToAction("AdminPanelMenagement2", "Admin");
            //}
            //else
            //{
            //    ViewBag.Message = " Geçersiz Yönetici. Admin mail veya şifre hatalı";
            //    return View();
            //}
            return RedirectToAction("AttendEvent", "MemberShip");

        }

    }
}
