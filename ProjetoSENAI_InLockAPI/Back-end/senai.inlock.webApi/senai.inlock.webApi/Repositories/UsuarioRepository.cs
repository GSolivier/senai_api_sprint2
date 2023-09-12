using Microsoft.Data.SqlClient;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;

namespace senai.inlock.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private string stringConexao = "Data Source = NOTE18-S14; Initial Catalog = inlock_games; User id = sa; pwd = Senai@134; TrustServerCertificate = true";
        //private string stringConexao = "Data Source = GUILHERME\\SQLEXPRESS; Initial Catalog = inlock_games; User id = sa; pwd = Senai@134; TrustServerCertificate = true";

        /// <summary>
        /// Método que realiza um login a partir de informações passadas pelo usuário
        /// </summary>
        /// <param name="email">email passado pelo usuário</param>
        /// <param name="senha">senha passada pelo usuário</param>
        /// <returns>retorna um token JWT</returns>
        public UsuarioDomain Login(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectUser = "SELECT IdUsuario, Usuario.IdTipoUsuario, Email, Titulo FROM Usuario LEFT JOIN TiposUsuario ON TiposUsuario.IdTipoUsuario = Usuario.IdTipoUsuario WHERE Email = @Email AND Senha = @Senha";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectUser, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    cmd.Parameters.AddWithValue("@Senha", senha);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuarioEncontrado = new UsuarioDomain()
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            Email = Convert.ToString(rdr["Email"]),

                            TipoUsuario = new TiposUsuarioDomain()
                            {

                                Titulo = Convert.ToString(rdr["Titulo"])
                            }
                        };

                        if (usuarioEncontrado.Email != null)
                        {
                            return usuarioEncontrado;
                        }
                    }
                }
            }

            return null;
        }
    }
}
