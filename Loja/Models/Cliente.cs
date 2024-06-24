using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        [Display(Name = "Endereço")]
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
        [Display(Name = "E-mail")]
        public string? Email { get; set; }
    }
}
