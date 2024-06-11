using BibliotecaServiceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BibliotecaServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Login == "admin" && login.Senha == "admin")
            {
                var token = GerarTokenJWT();
                return Ok(new { token });
            }
            return BadRequest(new { mensagem = "Credenciais inválidas. Por favor, verifique seu nome de usuário e senha " });
        }

        private string GerarTokenJWT()
        {
            string chaveSecreta = "53cd79e3-8114-4dcd-9e82-502924d7ef0c";
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            // Header
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admin"),
                new Claim("nome", "Administrador do Sistema")
            };

            var token = new JwtSecurityToken(
                issuer: "sua_empresa",
                audience: "sua_aplicacao",
            // Payload
                claims: null,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
