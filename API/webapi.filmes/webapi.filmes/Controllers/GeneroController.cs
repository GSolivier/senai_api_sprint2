using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.filmes.Domains;
using webapi.filmes.Interfaces;
using webapi.filmes.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// Objeto _generoRepository que irá receber todos os métodos definidos na interface IGeneroRepository
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _generoRepository para que haja referencia os métodos do repositório
        /// </summary>
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();
        }

        /// <summary>
        /// Endpoint que aciona o método ListarTodos no repositório GeneroRepository
        /// </summary>
        /// <returns>Retorna a resposta para o usuário(front-end)</returns>
        [HttpGet]
        [Authorize(Roles = "True, False")]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados da requisição
                List<GeneroDomain> listaGenero = _generoRepository.ListarTodos();

                //Retorna a lista no formato JSON com o status code Ok(200)
                return Ok(listaGenero);

            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que aciona o método BuscarPorId no repositório GeneroRepository
        /// </summary>
        /// <param name="idGenero"></param>
        /// <returns></returns>
        [HttpGet("{idGenero}")]
        [Authorize(Roles = "True, False")]
        public IActionResult GetById(int idGenero)
        {
            try
            {
                //guarda o objeto retornado no método do Repositorio no generoBuscado
                GeneroDomain generoBuscado = _generoRepository.BuscarPorId(idGenero);

                if (generoBuscado == null)
                {
                    return NotFound("Genero não encontrado");
                }

                //Retorna o generoBuscado
                return Ok(generoBuscado);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        /// Endpoint que aciona o método de cadastro de genero
        /// </summary>
        /// <param name="novoGenero">Objeto recebido na requisiçao</param>
        /// <returns>Retorna a resposta para o usuário(front-end)</returns>
        [HttpPost]
        [Authorize(Roles = "True")]
        public IActionResult Post(GeneroDomain novoGenero)
        {
            try
            {
                //Fazendo a chamada para o método cadastrar passando o objeto como parametro
                _generoRepository.Cadastrar(novoGenero);

                //Retorna um status code 201 - Created
                //return StatusCode(201);
                return Created("Criado o objeto", novoGenero);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// Endpoint que aciona o método de deletar genero
        /// </summary>
        /// <param name="idGenero">Id do objeto genero que será deletado</param>
        /// <returns>Retorna a resposta para o usuário(front-end)</returns>
        [HttpDelete("{idGenero}")]
        [Authorize(Roles = "True")]
        public IActionResult Delete(int idGenero)
        {
            try
            {
                //Fazendo o chamada de Deletar um genero usando como parametro o idGenero
                _generoRepository.Deletar(idGenero);


                //retorna um status code 204
                return NoContent();
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que atualiza um Genero pelo corpo
        /// </summary>
        /// <param name="genero">Objeto com os novos valores que serao atualizados</param>
        /// <returns>Retorna um Status Code 200 OK</returns>
        [HttpPut]
        [Authorize(Roles = "True")]
        public IActionResult PutBody(GeneroDomain genero)
        {
            try
            {

               GeneroDomain generoBuscado = _generoRepository.BuscarPorId(genero.IdGenero);

                if (generoBuscado == null)
                {
                    return NotFound("Genero não encontrado");
                }

                //Chama o método do GeneroRepository
                _generoRepository.AtualizarIdCorpo(genero);

                return Ok();
            }
            catch (Exception erro)
            {
                //Retorna um badrequest
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que atualiza um genero passando o seu Id pela URL
        /// </summary>
        /// <param name="genero">objeto que tera os valores a serem atualizados</param>
        /// <param name="idGenero">id que sera passado pela URL</param>
        /// <returns>Retorna um status code OK</returns>
        [HttpPut("{idGenero}")]
        [Authorize(Roles = "True")]
        public IActionResult PutUrl(GeneroDomain genero, int idGenero)
        {
            try
            {
                //Chama o método do GeneroRepository
                _generoRepository.AtualizarIdUrl(genero, idGenero);

                return Ok();
            }
            catch (Exception erro)
            {
                //Retorna um badrequest
                return BadRequest(erro.Message);
            }
        }
    }
}
