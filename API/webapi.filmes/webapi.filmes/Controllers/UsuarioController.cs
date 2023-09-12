using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi.filmes.Domains;
using webapi.filmes.Interfaces;
using webapi.filmes.Repositories;

namespace webapi.filmes.Controllers
{
    //Define que rota de uma requisição será no seguinte formato
    //dominio/api/nomeController
    //ex: http://localhost:5000/api/genero
    [Route("api/[controller]")]

    //define que é um controlador de API
    [ApiController]

    //Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    //Método controlador que herda da ControllerBase, onde será criado os Endpoints
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Objeto _usuarioRepository que irá receber todos os métodos definidos na interface IUsuarioRepository
        /// </summary>
        private IUsuarioRepository _usuarioRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _usuarioRepository para que haja referencia os métodos do repositório
        /// </summary>
        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }
        /// <summary>
        /// Endpoint que acessa o repoitory e chama o método de logar
        /// </summary>
        /// <param name="usuario">objeto que será procurado</param>
        /// <returns>retorna o status code ok</returns>
        [HttpPost]
        public IActionResult Get(UsuarioDomain usuario)
        {
            try
            {
                //Chama o método do repository e armazena em um objeto
                UsuarioDomain usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

                //Se o usuario for nulo, ele retorna um notfound
                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou senha inválidos!");
                }

                //Caso encontre o usuario, prossegue para a criação do token

                // 1 - Definir as informações (Claims) que serão fornecidas no token (PAYLOAD)

                var claims = new[]
                {
                    //Formato da claim
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Email),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Permissao.ToString()),

                    //existe a possibilidade de criar uma claim personalizada
                    new Claim("Claim Personalizada", "Valor da claim personalizada")
                };

                // 2 - Define a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));
                

                //3 - Definir as credencias do token (HEADER)
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4 - Gerar o Token
                var token = new JwtSecurityToken
                    (
                        //emissor do token
                        issuer: "webapi.filmes",

                        //Destinatario do token
                        audience: "webapi.filmes",

                        //dados definidos nas claims (informações)
                        claims: claims,
                        
                        //tempo de expiração do token
                        expires: DateTime.Now.AddMinutes(5),

                        //credencias do token
                        signingCredentials: credential

                    );

                //5 - retornar o token criado
                return Ok(new { 
                    
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                
                });
            }
            catch (Exception)
            {
                //retorna um badrequest
                return BadRequest();
            }
        }
    }
}
