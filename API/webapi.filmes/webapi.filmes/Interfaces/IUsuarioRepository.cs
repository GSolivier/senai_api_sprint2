using webapi.filmes.Domains;

namespace webapi.filmes.Interfaces
{
    /// <summary>
    /// Interface responsável pelo UsuarioRepository
    /// Definir os métodos que serão implementados
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Método para buscar um usuário através do email e senha
        /// </summary>
        /// <param name="email">email que o usário vai digitar</param>
        /// <param name="senha">senha que o usuario vai digitar</param>
        /// <returns>Retorna um objeto caso o email e senha correspondam.</returns>
        UsuarioDomain Login(string email, string senha);
    }
}
