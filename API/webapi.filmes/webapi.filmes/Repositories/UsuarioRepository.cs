using System.Data.SqlClient;
using webapi.filmes.Domains;
using webapi.filmes.Interfaces;

namespace webapi.filmes.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        //Autenticação com o SQL Server
        private string stringConexao = "Data Source = NOTE18-S14; Initial Catalog = Filmes; User id = sa; pwd = Senai@134; TrustServerCertificate = true";

        /// <summary>
        /// Método para buscar um usuário através do email e senha
        /// </summary>
        /// <param name="email">email que o usário vai digitar</param>
        /// <param name="senha">senha que o usuario vai digitar</param>
        /// <returns>Retorna um objeto caso o email e senha correspondam.</returns>
        public UsuarioDomain Login(string email, string senha)
        {
            //declara o SqlConnection passando como parametro a string de conexao
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //comando que será executado
                string querySelectUser = "SELECT IdUsuario, Email, Permissao FROM Usuario WHERE Email = @Email AND Senha = @Senha";

                //Abre a conexão com o banco de dados
                con.Open();

                //Declara um SqlDataReader
                SqlDataReader rdr;

                //DEclara a SqlCOmmanf passando como parametro a conexao com o banco de dados e a query
                using (SqlCommand cmd = new SqlCommand(querySelectUser, con))
                {
                    //coloca o valor do email digitado a variavel na query
                    cmd.Parameters.AddWithValue("@Email", email);

                    //coloca o valor da senha digitada a variavel na query
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    //Le os dados da tabela e armazena no rdr
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        //Declara um novo usuario que vai guardar o usuario achado
                        UsuarioDomain usuarioEncontrado = new UsuarioDomain() {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            Email = Convert.ToString(rdr["Email"]),
                            Permissao = Convert.ToBoolean(rdr["Permissao"])
                        };

                        //caso o email seja diferente de nulo, ele retorna um objeto com o usuario encontrado
                        if (usuarioEncontrado.Email != null)
                        {
                            //retorna o usuario encontrado
                            return usuarioEncontrado;
                        }
                       
                    }
                }
            }

            //caso nao ache o usuario, retorna um valor nulo
            return null;
        }
    }
}
