using Bitime_Projesi_API.Models;
using EtkinLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bitime_Projesi_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        EtkinLinkContext context = new EtkinLinkContext();
        
        
        
        [HttpPost]
        public string SingUp(DevSingUpModel dev)
        {
            var DevUser = context.DevTBLs.FirstOrDefault(x => x.DevMail == dev.Mail && x.DevPassword==dev.Password);
            if (DevUser == null)
            {
                DevTBL developer = new DevTBL();
                developer.DevMail=dev.Mail;
                developer.DevPassword=dev.Password;
                developer.DevSite=dev.Site;
                context.DevTBLs.Add(developer);
                context.SaveChanges();
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Convert.FromBase64String("bohrbohrbohrbohrbohrbohrbohrbohr"));
                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, dev.Mail));
                claims.Add(new Claim(ClaimTypes.Name, dev.Site));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer:"localhost",
                    audience:"localhost",
                    claims:claims,
                    expires:DateTime.Now.AddDays(30),
                    signingCredentials:signingCredentials
                    );
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                string devtoken = handler.WriteToken(token);
               
                return devtoken;

            }
            else
            {
                return "Kayıtlı Kullanıcı";
            }
        }

  
    }
}
