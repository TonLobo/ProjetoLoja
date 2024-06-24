using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }
        [Display(Name = "Usuário")]
        public string? Username { get; set; }
        [Display(Name = "Senha")]
        public string? Password { get; set; }
        [Display(Name = "Cargo")]
        public string? Role { get; set; } // "Gerente" ou "Vendedor"
    }
}
