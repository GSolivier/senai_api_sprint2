using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi.Domains
{
    /// <summary>
    /// Classe que reprenta a classe usuario
    /// </summary>
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }

        public int IdTipoUsuario { get; set; }


        [Required(ErrorMessage = "O email é obrigatório")]
        public string? Email { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "A senha deve ter no mínimo 3 caracteres.")]
        [Required(ErrorMessage = "A Senha é obrigatória")]
        public string? Senha { get; set; }

        public TiposUsuarioDomain TipoUsuario { get; set; }
    }
}
