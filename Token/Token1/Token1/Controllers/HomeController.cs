using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Token1.Filters;
using Token1.Models;

namespace Token1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpPost("login")]
        public IActionResult Loggati(UserLogin objLogin)
        {
            if (objLogin.UserName == "enrico" && objLogin.Password == "password")
            {
                List<Claim> claims = new List<Claim>()
                {
                    //claim è standard mvc
                    new Claim(JwtRegisteredClaimNames.Sub, objLogin.UserName),
                    //il claim è il contenuto key-value salvato nel playload del token
                    new Claim("UserType", "ADMIN"),
                    //evito che due token abbiano lo stesso JWT
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                //ci basiamo su una chiave simmetrica unica per cifrare e decifrare.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_your_super_secret_key"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Archety.dev",
                    audience: "Popolo",
                    claims: claims,          //Body o Payload del JWT
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                    );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }


        [HttpGet("utenteprofilo")]
        [AutorizzaUtentePerTipo("USER")]
        public IActionResult DammiInformazioniUtente()
        {
            return Ok(new Risposta()
            {
                Status = "SUCCESS",
                Data = "Dati sensibili USER"
            });
        }

        [HttpGet("adminprofilo")]
        [AutorizzaUtentePerTipo("ADMIN")]
        public IActionResult DammiInformazioniAmministratore()
        {
            return Ok(new Risposta()
            {
                Status = "SUCCESS",
                Data = "Dati sensibili ADMIN"
            });
        }
    }
}
