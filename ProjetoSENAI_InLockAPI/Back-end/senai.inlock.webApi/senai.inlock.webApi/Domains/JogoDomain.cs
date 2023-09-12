using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade jogo
    /// </summary>
    public class JogoDomain
    {
        public int IdJogo { get; set; }

        public int IdEstudio { get; set; }

        [Required(ErrorMessage = "O nome do jogo é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public string? Valor { get; set; }

        public EstudioDomain? Estudio { get; set; }
    }
}
