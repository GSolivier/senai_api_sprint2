using webapi.filmes.Domains;

namespace webapi.filmes.Interfaces
{
    public interface IFilmeRepository
    {
        //tipo de retorno NomeMetodo(Parametro nomeParametro)

        /// <summary>
        /// Cadastrar um novo filme
        /// </summary>
        /// <param name="novoFilme">Objeto que será cadastrado</param>
        void Cadastrar(FilmeDomain novoFilme);

        /// <summary>
        /// Listar todos os objetos cadastrados
        /// </summary>
        /// <returns>Lista com os objetos</returns>
        List<FilmeDomain> ListarTodos();

        /// <summary>
        /// Busca um objeto atráves do seu id
        /// </summary>
        /// <param name="idFilme">id do objeto a ser buscado</param>
        /// <returns>objeto postado</returns>
        FilmeDomain BuscarPorId(int idFilme);

        /// <summary>
        /// Atualiza um objeto existente passando o seu id pelo corpo da requisição
        /// </summary>
        /// <param name="filme">Objeto atualizado com as novas informações</param>
        void AtualizarIdCorpo(FilmeDomain filme);

        /// <summary>
        /// Atualiza um objeto existente passando o seu id pela URL
        /// </summary>
        /// <param name="filme">Objeto atualizado com as novas informações</param>
        /// <param name="idFilme">Id do objeto que será atualizado</param>
        void AtualizarIdUrl(FilmeDomain filme, int idFilme);

        /// <summary>
        /// deleta um objeto
        /// </summary>
        /// <param name="idFilme">objeto que sera deletado</param>
        void Deletar(int idFilme);
    }
}
