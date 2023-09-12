using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    /// <summary>
    /// Interface que representa os métodos que serão implementados no repositorio de Jogo
    /// </summary>
    public interface IJogoRepository
    {
        /// <summary>
        /// Método que lista todos os objetos da entidade JogoDomain
        /// </summary>
        /// <returns>Retorna uma lista com todos os objetos</returns>
        List<JogoDomain> ListarTodos();

        /// <summary>
        /// Método que cadastra um novo jogo
        /// </summary>
        /// <param name="novoJogo">objeto com as novas informações que serão implementadas</param>
        void Cadastrar (JogoDomain novoJogo);
    }
}
