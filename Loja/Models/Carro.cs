using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class Carro
    {
        public int CarroId { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public int Ano { get; set; }
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        [Display(Name = "Novo")]
        public bool IsNovo { get; set; }
        public int Estoque { get; set; }
        public string? Url { get; set; }
    }
}
