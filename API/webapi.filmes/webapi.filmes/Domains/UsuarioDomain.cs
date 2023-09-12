using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.Domains
{
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório!")]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage ="A senha deve ter no mínimo 3 caracteres.")]
        [Required(ErrorMessage ="O campo senha é obrigatório")]
        public string Senha { get; set; }
        public bool Permissao { get; set; }

    }
}
