using Bitime_Projesi_API.Models;
using EtkinLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bitime_Projesi_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DevGetController : ControllerBase
    {
        EtkinLinkContext context = new EtkinLinkContext();
       

        [HttpPost]
        public IActionResult SendEtkinlink(SendEtkinlik send)
        {
            EtkinlikGet_tbl etkinlik = new EtkinlikGet_tbl();
            etkinlik.EtkinlikAdı=send.etkinlikadı;
            etkinlik.EtkinlikFiyatı=send.etkinlikfiyat;
            context.EtkinlikGet_Tbls.Add(etkinlik);
            context.SaveChanges();
            return Ok();
        }

        [Authorize]
        public IActionResult Get()
        {
            var data = context.EtkinlikGet_Tbls.FirstOrDefault();
            SendEtkinlik send = new SendEtkinlik();
            send.etkinlikadı=data.EtkinlikAdı;
            send.etkinlikfiyat=data.EtkinlikFiyatı;
            context.EtkinlikGet_Tbls.Remove(data);
            context.SaveChanges();
            
            return Ok(send);
        }
    }
}
