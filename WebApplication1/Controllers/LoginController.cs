using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]// attribute based routing
    [ApiController]
    [AllowAnonymous] // indicating anyone can login can authenticate
    public class LoginController : ControllerBase
    { private readonly IConfiguration _configuration; // needed for authentication as it allows to fetch auth related functionalities or settings like JWT secret keys , expiration times , etc
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public ActionResult Login(Logindto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("provide proper credentials");
            }
            LoginResponsedto response = new LoginResponsedto()
            {
                username = model.username
            };
            string audience = string.Empty;
            string issuer = string.Empty;
            byte[] key = null;
            if (model.Policy == "Local")
            {
                issuer = _configuration.GetValue<string>("LocalIssuer");
                audience = _configuration.GetValue<string>("LocalAudience");
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretforLocal"));

            }
            else if(model.Policy =="Microsoft"){
                issuer = _configuration.GetValue<string>("MicrosoftIssuer");
                audience = _configuration.GetValue<string>("MicrosoftAudience");
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretforMicrosoft"));

            }
            else if(model.Policy=="Google"){
                issuer = _configuration.GetValue<string>("GoogleIssuer");
                audience = _configuration.GetValue<string>("GoogleAudience");
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretforGoogle"));


            }
            if (model.username == "Satish Kumar" && model.password == "satish") {
                var tokenHandler = new JwtSecurityTokenHandler(); // used for creating JWT tokens , validating , serializing and deserializing
                var tokenDescriptor = new SecurityTokenDescriptor() // used for defining structure of JWT token
                {
                    Issuer = issuer,
                    Audience = audience,
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,model.username),
                        new Claim(ClaimTypes.Role,"Admin")

                    }),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.token = tokenHandler.WriteToken(token);
            }
            else
            {
                return Ok("invalid credentials");
            }
            return Ok(response);


        }
    }
}
