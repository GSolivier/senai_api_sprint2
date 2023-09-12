using Microsoft.Data.SqlClient;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Globalization;

namespace senai.inlock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private string stringConexao = "Data Source = NOTE18-S14; Initial Catalog = inlock_games; User id = sa; pwd = Senai@134; TrustServerCertificate = true";
        //private string stringConexao = "Data Source = GUILHERME\\SQLEXPRESS; Initial Catalog = inlock_games; User id = sa; pwd = Senai@134; TrustServerCertificate = true";


        public List<EstudioDomain> ListarTodos()
        {
            List<EstudioDomain> listaEstudios = new List<EstudioDomain>();


            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT Estudio.IdEstudio, Estudio.Nome AS Estudio, IdJogo, Jogo.Nome AS Jogo, Descricao, DataLancamento, Valor FROM Estudio LEFT JOIN Jogo ON Jogo.IdEstudio = Estudio.IdEstudio ORDER BY Estudio.IdEstudio";


                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    Dictionary<int, EstudioDomain> estudiosDict = new Dictionary<int, EstudioDomain>();



                    while (rdr.Read())
                    {
                        int idEstudio = Convert.ToInt32(rdr["IdEstudio"]);

                        if (!estudiosDict.ContainsKey(idEstudio))
                        {
                            EstudioDomain estudio = new EstudioDomain()
                            {
                                IdEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                                Nome = Convert.ToString(rdr["Estudio"]),

                                jogos = new List<JogoDomain>()
                            };
                            estudiosDict.Add(idEstudio, estudio);
                        }



                        JogoDomain jogos = new JogoDomain()
                        {
                            IdJogo = Convert.ToInt32(rdr["IdJogo"]),
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"]),

                            Nome = Convert.ToString(rdr["Jogo"]),

                            Descricao = Convert.ToString(rdr["Descricao"]),
                            DataLancamento = Convert.ToDateTime(rdr["DataLancamento"]),
                            Valor = Convert.ToString(rdr["Valor"])
                        };

                        estudiosDict[idEstudio].jogos.Add(jogos);




                        listaEstudios = estudiosDict.Values.ToList();
                    }
                }
            }
            return listaEstudios;
        }
    }
}
