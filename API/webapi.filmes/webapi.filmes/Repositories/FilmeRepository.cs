using System.Data.SqlClient;
using System.Globalization;
using webapi.filmes.Domains;
using webapi.filmes.Interfaces;
using static FsCheck.TypeClass.InstanceKind;

namespace webapi.filmes.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {

        /// <summary>
        /// String de conexão com o banco de dados que recebe os seguintes parâmetros:
        /// Data Source: Nome do Servidor
        /// Initial Catalog: Nome do banco de dados
        /// Autenticação com o windowns
        /// private string stringConexao = "Data Source = NOTE18-S14; Initial Catalog = Filmes; Integrated Security = true";
        /// </summary>

        //Autenticação com o SQL Server
        private string stringConexao = "Data Source = NOTE18-S14; Initial Catalog = Filmes; User id = sa; pwd = Senai@134; TrustServerCertificate = true";

        /// <summary>
        /// Método que atualiza um filme pelo corpo da requisição
        /// </summary>
        /// <param name="filme">objeto com os novos valores</param>
        public void AtualizarIdCorpo(FilmeDomain filme)
        {
            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a instrução a ser executada
                string queryUpdateBody = "UPDATE Filme SET Titulo = @Titulo, IdGenero = @IdGenero WHERE IdFilme = @IdFilme";

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dado
                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                {
                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);

                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdGenero",filme.IdGenero);

                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdFilme", filme.IdFilme);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Método que atualiza um filme pela URL
        /// </summary>
        /// <param name="filme">objeto que tera os novos valores</param>
        /// <param name="idFilme">id que será passado na URL</param>
        public void AtualizarIdUrl(FilmeDomain filme, int idFilme)
        {
            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a instrução a ser executada
                string queryUpdateBody = "UPDATE Filme SET Titulo = @Titulo, IdGenero = @IdGenero WHERE IdFilme = @IdFilme";

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dado
                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                {
                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);

                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdGenero", filme.IdGenero);

                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdFilme", idFilme);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }
    

        /// <summary>
        /// Método que busca um objeto através do seu ID
        /// </summary>
        /// <param name="idFilme">Id do filme que será procurado</param>
        /// <returns>Retorna o objeto encontrado</returns>
        public FilmeDomain BuscarPorId(int idFilme)
        {
            //Declara um objeto que sera guardado o filme buscado.
            FilmeDomain filmeBuscado = new FilmeDomain();

            //Declara uma nova SqlConnection passando como parametro a string de conexao
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Comando que será executado
                string querySelectAll = "SELECT IdFilme, Filme.IdGenero, Titulo, Nome FROM Filme LEFT JOIN Genero ON Genero.IdGenero = Filme.IdGenero";

                //abre a conexão do banco de dados
                con.Open();

                //Declara o datareader que percorre a tabela do banco de dados
                SqlDataReader rdr;

                //declara o SqlCommand passando a query e a con como parametros
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //Executa o reader e atribui ao rdr
                    rdr = cmd.ExecuteReader();

                    
                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            //Atribui a propriedade IdFilme com o valor recebido no rdr
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),

                            //Atribui a propriedade IdGenero com o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            //Atribui a propriedade Titulo com o valor recebido no rdr
                            Titulo = Convert.ToString(rdr["Titulo"]),

                            //Atribui a propriedade Genero um objeto do tipo GeneroDomain, trazendo as informações correspondentes aquele filme
                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                                Nome = Convert.ToString(rdr["Nome"])
                            }
                        };

                        //Condição para atribuir o filme que seja igual ao id digitado
                        if (filme.IdFilme == idFilme)
                        {
                            //atribui o filme ao filme buscado
                            filmeBuscado = filme;

                            //retorna o filme buscado
                            return filmeBuscado;
                        }
                    }
                }
            }
            //caso nao exista o id digitado, retorna nulo.
            return null;
        }

        /// <summary>
        /// Cadastra um novo filme na tabela
        /// </summary>
        /// <param name="novoFilme">Objeto com os valores que serão adicionados na tabela</param>
        public void Cadastrar(FilmeDomain novoFilme)
        {
            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a Query que sera executada
                string queryInsert = "INSERT INTO Filme(IdGenero, Titulo) VALUES (@IdGenero , @Titulo)";

                //Declara a SqlCommand passando a query que sera executada e a conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //passa o valor do parametro para a variavel no banco de dados
                    cmd.Parameters.AddWithValue("@IdGenero", novoFilme.IdGenero);

                    //passa o valor do parametro para a variavel no banco de dados
                    cmd.Parameters.AddWithValue("@Titulo", novoFilme.Titulo );

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deleta um filme da tabela
        /// </summary>
        /// <param name="idFilme">Id do filme que será deletado</param>
        public void Deletar(int idFilme)
        {
            //Declara a SqlConnection passando como parametro a string de conexao
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //comando que será executado
                string queryDelete = "DELETE FROM Filme WHERE IdFilme = @IdDelete";

                //Declara o SqlCommand passando a query e a con como parametro
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    //Passa o valor idFilme para a variavel no banco de dados
                    cmd.Parameters.AddWithValue("@IdDelete", idFilme);

                    //Abre a conexao com o banco de dados
                    con.Open();

                    //Executa a query.
                    cmd.ExecuteNonQuery();
                }

            }
        }

        /// <summary>
        /// Listar todos os objetos filmes trazendo tambem o seu genero
        /// </summary>
        /// <returns>Retorna a lista completa com todos os filmes</returns>
        public List<FilmeDomain> ListarTodos()
        {
            //Cria uma lista de objetos do tipo Filme
            List<FilmeDomain> listaFilme = new List<FilmeDomain>();

            //Declara a SqlConneciton passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a query que será executada
                string querySelectAll = "SELECT IdFilme, Filme.IdGenero, Titulo, Nome FROM Filme LEFT JOIN Genero ON Genero.IdGenero = Filme.IdGenero";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara um SqlDataReader para percorrer a tabela do banco
                SqlDataReader rdr;

                //Declara o SqlCommand passando a query que será executada e conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    //Executa uma função enquanto o rdr estiver rodando
                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            //Atribui a propriedade IdFilme com o valor recebido no rdr
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),

                            //Atribui a propriedade IdGenero com o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            //Atribui a propriedade Titulo com o valor recebido no rdr
                            Titulo = Convert.ToString(rdr["Titulo"]),
                            
                            //Atribui a propriedade Genero um objeto do tipo GeneroDomain, trazendo as informações correspondentes aquele filme
                            Genero = new GeneroDomain()
                            {
                                IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                                Nome = Convert.ToString(rdr["Nome"])
                            }
                        };

                        //Adiciona cada objeto na lista
                        listaFilme.Add(filme);
                    }
                }
            }
            //retorna a lista de filmes.
            return listaFilme;
        }
    }
}
