using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetoTecnico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Endpoint para obtener el Token.
        [HttpGet("obtener-token")]
        public IActionResult GenerarToken()
        {
            // Leemos la clave secreta 
            var jwtKey = _configuration["Jwt:Key"] ?? "UnaClaveSecretaMuyLargaParaElRetoDeContinental2026!";
            var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "usuario_prueba"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Algoritmo de encriptación y creación del token
            var credentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // El token dura 1 hora
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Retornamos el token en formato string
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}