using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.inlock.codeFirst.manha.Domains
{
    /// <summary>
    /// Classe que representa a entidade jogo
    /// </summary>
    [Table("Jogo")]
    public class Jogo
    {
        [Key]
        public Guid IdJogo { get; set; } = Guid.NewGuid();

        [Column(TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage = "O nome do jogo é obrigatório")]
        public string Nome { get; set; }

        [Column(TypeName = "TEXT")]
        [Required(ErrorMessage = "A descricao do jogo é obrigatório")]
        public string Descricao { get; set; }

        [Column(TypeName = "DATE")]
        [Required (ErrorMessage = "A data de lançamento é obrigatória")]
        public DateTime DataLancamento { get; set; }

        [Column(TypeName = "SMALLMONEY")]
        [Required(ErrorMessage = "O valor é obrigatório")]
        public decimal Valor { get; set; }

        //Referencia da chave estrangeira.

        [Required(ErrorMessage = "O IdEstudio é obrigatório")]
        public Guid IdEstudio { get; set; }

        [ForeignKey("IdEstudio")]
        public Estudio Estudio { get; set; }
    }
}
