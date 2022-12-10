using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebService.Data.Model;
using WebService.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Congé
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromBody] Utilisateur user)
        {
            var dbUser = _context.Utilisateurs.Where(u => u.Email == user.Email && u.MotDePasse == user.MotDePasse).FirstOrDefault();
            if (dbUser == null)
            {
                return BadRequest("l'email ou le mot de passe sont incorrecte");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, dbUser.Email),
                new Claim("userID",dbUser.Id.ToString()),
                new Claim("userTemps",dbUser.TempsTravail.ToString()),
                new Claim("userSolde",dbUser.SoldeCongé.ToString())

                

            };
            var token = getToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo

            });
        }
        private JwtSecurityToken getToken(List<Claim> authClaim)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(3),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }


    }
}
