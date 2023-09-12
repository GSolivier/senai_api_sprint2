using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;

namespace senai.inlock.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudioController : ControllerBase
    {
        private IEstudioRepository _estudioRepository;

        public EstudioController()
        {
            _estudioRepository = new EstudioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                List<EstudioDomain> listaEstudios = _estudioRepository.ListarTodos();

                

                return Ok(listaEstudios);

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
            

        }
    }
}
