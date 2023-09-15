using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.inlock.codeFirst.manha.Domains
{
    [Table("Usuario")]
    [Index(nameof(Email), IsUnique = true)] //Cria um índice único
    public class Usuario
    {
        [Key]
        public Guid IdUsuario { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        [Required(ErrorMessage = "O email é obrigatório")]
        public string? Email { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 20 caracteres")]
        public string? Senha { get; set; }

        //Referencia da chave estrangeira (Tabela TipoUsuario)

        [Required(ErrorMessage = "O id é obrigatório")]
        public Guid IdTipoUsuario { get; set; }

        [ForeignKey("IdTipoUsuario")]
        public TiposUsuario? TiposUsuario { get; set; }
    }
}
