using System.Data.SqlClient;
using webapi.filmes.Domains;
using webapi.filmes.Interfaces;

namespace webapi.filmes.Repositories
{
    public class GeneroRepository : IGeneroRepository
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
        /// Atualiza um genero passando o seu id pelo corpo do JSON
        /// </summary>
        /// <param name="genero">Objeto com os valores passados pelo usuario</param>
        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a instrução a ser executada
                string queryUpdateBody = "UPDATE Genero SET Nome = @Nome WHERE IdGenero = @IdGenero";

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dado
                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                {
                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);

                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdGenero", genero.IdGenero);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Atualiza um genero passando o Id pela URL
        /// </summary>
        /// <param name="genero">objeto passando os atributos a serem atualizados</param>
        /// <param name="idGenero">Id que vai ser passado pela URL</param>
        public void AtualizarIdUrl(GeneroDomain genero, int idGenero)
        {
            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a instrução a ser executada
                string queryUpdateBody = "UPDATE Genero SET Nome = @Nome WHERE IdGenero = @IdGenero";

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dado
                using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                {
                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);

                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdGenero", idGenero);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Buscar um genero pelo seu id
        /// </summary>
        /// <param name="idGenero">id que sera passado como parametro de busca</param>
        /// <returns>retorna o objeto encontrado</returns>
        public GeneroDomain BuscarPorId(int idGenero)
        {
            //Estancia um objeto para ser buscado
            GeneroDomain generoBuscado = new GeneroDomain();

            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                //Declara a instrução a ser executada
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara o SqlDataReader para percorrer a tabela do banco
                SqlDataReader rdr;

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {

                            //atribui a propriedade IdGenero o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            //atribui a propriedade Nome o valor recebido no rdr
                            Nome = Convert.ToString(rdr["Nome"])

                        };

                        //Condição comparando o id digitado pelo usuario com todos os id's da tabela
                        if (genero.IdGenero == idGenero)
                        {
                            //atribui o objeto corresponde a variavel generoBuscado
                            generoBuscado = genero;

                            //Retorna o objeto genero buscado
                            return generoBuscado;
                        }
                    }

                }
            }
           
            //retorna nulo caso o genero informado nã exista
            return null;
        }


        /// <summary>
        /// Cadastrar um novo Genero
        /// </summary>
        /// <param name="novoGenero">objeto com as informações que serão cadastradas</param>
        public void Cadastrar(GeneroDomain novoGenero)
        {

            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a instrução a ser executada
                string queryInsert = "INSERT INTO Genero(Nome) VALUES (@Nome)";


                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.Nome);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a query (queryInsert)
                    cmd.ExecuteNonQuery();

                }
            }

        }

        /// <summary>
        /// Endpoint que deleta um objeto da tabela Genero
        /// </summary>
        /// <param name="idGenero">Id do genero que vai ser deletado</param>
        public void Deletar(int idGenero)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a instrução a ser executada
                string queryDelete = "DELETE FROM Genero WHERE IdGenero = @IdDelete";

                //Declara a SqlConnection passando a string de conexao como parametro
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    //Passa o valor do parametro 
                    cmd.Parameters.AddWithValue("@IdDelete", idGenero);

                    //Abre a conexão com o database
                    con.Open();

                    //Executa a query Deletado
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Listar todos os objetos gêneros
        /// </summary>
        /// <returns>Lista de objetos (generos)</returns>
        public List<GeneroDomain> ListarTodos()
        {
            //Cria uma lista de objetos do tipo gênero
            List<GeneroDomain> listaGenero = new List<GeneroDomain>();

            //Declara a SqlConnection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a instrução a ser executada
                string querySelectAll = "SELECT IdGenero, Nome FROM Genero";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara o SqlDataReader para percorrer a tabela do banco
                SqlDataReader rdr;

                //Declara o SqlCommand passando a query que será executada e a conexão com o banco de dados
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    //Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {

                            //atribui a propriedade IdGenero o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),

                            //atribui a propriedade Nome o valor recebido no rdr
                            Nome = Convert.ToString(rdr["Nome"])

                        };

                        //Adiciona cada objeto dentro da lista
                        listaGenero.Add(genero);
                    }

                }


            }

            //retorna a lista de generos
            return listaGenero;
        }
    }
}
