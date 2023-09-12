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
    [Route("api/[controller]")]

    //define que é um controlador de API
    [ApiController]

    //Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    //Método controlador que herda da ControllerBase, onde será criado os Endpoints
    public class FilmeController : ControllerBase
    {
        /// <summary>
        /// Objeto _filmeRepository que irá receber todos os métodos definidos na interface IFilmeRepository
        /// </summary>
        private IFilmeRepository _filmeRepository { get; set; }

        /// <summary>
        /// instancia o objeto _filmeRepository para que haja referencia os métodos do repositório
        /// </summary>
        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// Endpoint que aciona o método ListarTodos no repositório FilmeRepository
        /// </summary>
        /// <returns>Retorna um status code 200 - Ok junto com a lista</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados do método Listar todos
                List<FilmeDomain> listaFilme = _filmeRepository.ListarTodos();

                //Retorna a lista completa com todos os filmes, e um status code 200
                return Ok(listaFilme);

            }
            catch (Exception erro)
            {
                //Retorna um BadREquest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// Endpoint que chama o método buscar por id no repository
        /// </summary>
        /// <param name="idFilme">id que sera buscado</param>
        /// <returns>retorna um status code 200 - Ok passando o objeto encontrado</returns>
        [HttpGet("{idFilme}")]
        public IActionResult GetById(int idFilme)
        {
            try
            {
                //Guarda o filme encontrado na variavel
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(idFilme);

                //caso seja nulo, entra nessa condição
                if (filmeBuscado == null)
                {
                    //retorna um notfound com uma mensagem
                    return NotFound("Filme não encontrado");
                }

                //retorna o filme buscado
                return Ok(filmeBuscado);
            }
            catch (Exception erro)
            {
                //Retorna um BadREquest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que chama o método Cadastrar do FilmeRepository
        /// </summary>
        /// <param name="novoFilme">Objeto com as informações a serem adicionadas</param>
        /// <returns>Retorna um status code Created, passando as informaçoes inseridas</returns>
        [HttpPost]
        public IActionResult Post(FilmeDomain novoFilme) 
        {
            try
            {
                //Acessa o repositório e busca o método de cadastrar passando como parametro o novoFIlme
                _filmeRepository.Cadastrar(novoFilme);

                //Retorna as informações inseridas
                return Created("Criado o novo filme", novoFilme);
            }
            catch (Exception erro)
            {
                //Retorna um BadREquest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que chama o método deletar do FilmeRepository
        /// </summary>
        /// <param name="idFilme">Id do filme que será deletado</param>
        /// <returns>Retorna um status code de NoContent</returns>
        [HttpDelete]
        public IActionResult Delete(int idFilme)
        {
            try
            {
                //Chama o método deletar passando o idFilme como parametro
                _filmeRepository.Deletar(idFilme);

                //retorna um status code 204 - No content
                return NoContent();
            }
            catch (Exception erro)
            {
                //Retorna um BadREquest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que chama o método AtualizarIdCorpo do Repository
        /// </summary>
        /// <param name="filme">objeto com os novos valores</param>
        /// <returns>Status code 200 - Ok</returns>
        [HttpPut]
        public IActionResult PutBody(FilmeDomain filme)
        {
            try
            {
                // busca o filme digitado
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(filme.IdFilme);

                //se ele for nulo, entra nessa condição
                if (filmeBuscado == null)
                {
                    //retorna um notfound com uma mensagem para o usuario
                    return NotFound("Filme não encontrado");
                }

                //caso passe pela condição, atualiza o filme selecionado
                _filmeRepository.AtualizarIdCorpo(filme);

                //retorna o status code 200 - Ok
                return Ok();
            }
            catch (Exception erro)
            {
                //Retorna um BadREquest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint que atualiza um filme a partir da URL
        /// </summary>
        /// <param name="filme">objeto com os valores a serem atualizados</param>
        /// <param name="idFilme">id que sera passado na URL</param>
        /// <returns>Retorna um status code Ok - 200</returns>
        [HttpPut("{idFilme}")]
        public IActionResult PutUrl(FilmeDomain filme, int idFilme)
        {
            try
            {
                // busca o filme digitado
                FilmeDomain filmeBuscado = _filmeRepository.BuscarPorId(idFilme);

                //se ele for nulo, entra nessa condição
                if (filmeBuscado == null)
                {
                    //retorna um notfound com uma mensagem para o usuario
                    return NotFound("Filme não encontrado");
                }

                _filmeRepository.AtualizarIdUrl(filme, idFilme);

                //retorna o status code 200 - Ok
                return Ok();
            }
            catch (Exception erro)
            {
                //Retorna um BadREquest caso alguma exceção ocorra
                return BadRequest(erro.Message);
            }
        }
    }
}
