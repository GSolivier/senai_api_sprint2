using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    /// <summary>
    /// Interface que representa os métodos que serão implementados
    /// </summary>
    public interface IEstudioRepository
    {
        /// <summary>
        /// Método que lista todos os objetos da entidade estudio
        /// </summary>
        /// <returns>Retorna uma lista com os objetos</returns>
        List<EstudioDomain> ListarTodos();
    }
}
