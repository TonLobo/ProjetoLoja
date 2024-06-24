using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class Desconto
    {
        public int DescontoId { get; set; }
        public int VendaId { get; set; }
        public Venda? Venda { get; set; }
        [Display(Name = "Valor do Desconto")]
        public decimal ValorDesconto { get; set; }
        public bool Aprovado { get; set; }

    }
}
