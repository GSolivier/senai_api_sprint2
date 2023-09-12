using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace senai.inlock.webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade Estudio
    /// </summary>
    public class EstudioDomain
    {
        public int IdEstudio { get; set; }

        [Required(ErrorMessage = "O nome do estúdio é obrigatório")]
        public string Nome { get; set; }

        
        public List<JogoDomain> jogos{ get; set; }
    }
}
