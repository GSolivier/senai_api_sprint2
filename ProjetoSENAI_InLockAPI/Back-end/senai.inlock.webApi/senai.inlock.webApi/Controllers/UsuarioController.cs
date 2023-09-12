using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace senai.inlock.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }


        /// <summary>
        /// Endpoint que aciona o método login do UsuarioRepository
        /// </summary>
        /// <param name="usuario">informações que serão passadas no corpo da requisição</param>
        /// <returns>retorna o token de acesso com as informações do usuário</returns>
        [HttpPost]
        public IActionResult Post(UsuarioDomain usuario)
        {

            try
            {
                UsuarioDomain usuarioEncontrado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                if (usuarioEncontrado == null)
                {
                    return NotFound("Email ou senha inválidos");
                }

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.IdTipoUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.Email),
                new Claim(ClaimTypes.Role, usuarioEncontrado.TipoUsuario.Titulo)
            };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("senai-inlock-chave-autenticacao-webapi-dev"));

                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (
                        issuer: "senai.inlock.webApi",

                        audience: "senai.inlock.webApi",

                        claims: claims,

                        expires: DateTime.Now.AddMinutes(5),

                        signingCredentials: credential
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
           
        }
    }
}
