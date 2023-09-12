using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    /// <summary>
    /// Interface que representa os métodos que serão implementados no repositório Usuario
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Método que realiza um login a partir de informações passadas pelo usuário
        /// </summary>
        /// <param name="email">email passado pelo usuário</param>
        /// <param name="senha">senha passada pelo usuário</param>
        /// <returns>retorna um token JWT</returns>
        UsuarioDomain Login (string email, string senha);
    }
}
