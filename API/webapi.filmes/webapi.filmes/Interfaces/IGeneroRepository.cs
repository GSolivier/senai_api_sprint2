using webapi.filmes.Domains;

namespace webapi.filmes.Interfaces
{

    /// <summary>
    /// Interface responsável pelo repositório GeneroRepository
    /// Definir os métodos que serão implementados pelo GeneroRepository
    /// </summary>
    public interface IGeneroRepository
    {
        //tipo de retorno NomeMetodo(Parametro nomeParametro)

        /// <summary>
        /// Cadastrar um novo genero
        /// </summary>
        /// <param name="novoGenero">Objeto que será cadastrado</param>
        void Cadastrar(GeneroDomain novoGenero);

        /// <summary>
        /// Listar todos os objetos cadastrados
        /// </summary>
        /// <returns>Lista com os objetos</returns>
        List<GeneroDomain> ListarTodos();

        /// <summary>
        /// Busca um objeto atráves do seu id
        /// </summary>
        /// <param name="idGenero">id do objeto a ser buscado</param>
        /// <returns>objeto postado</returns>
        GeneroDomain BuscarPorId(int idGenero);

        /// <summary>
        /// Atualiza um objeto existente passando o seu id pelo corpo da requisição
        /// </summary>
        /// <param name="genero">Objeto atualizado com as novas informações</param>
        void AtualizarIdCorpo(GeneroDomain genero);

        /// <summary>
        /// Atualiza um objeto existente passando o seu id pela URL
        /// </summary>
        /// <param name="genero">Objeto atualizado com as novas informações</param>
        /// <param name="idGenero">Id do objeto que será atualizado</param>
        void AtualizarIdUrl(GeneroDomain genero, int idGenero);

        /// <summary>
        /// deleta um objeto
        /// </summary>
        /// <param name="idGenero">objeto que sera deletado</param>
        void Deletar(int idGenero);
    }
}
