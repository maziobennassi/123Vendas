using System.ComponentModel.DataAnnotations;

namespace _123Vendas.Vendas.Application.DTOs
{
    public class ProdutoDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid VendaId { get; set; }
        [Required]
        public string Nome { get; set; } = default!;
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public double Desconto { get; set; }
        [Required]
        public double ValorTotal { get; set; }
        [Required]
        public bool Cancelado { get; set; }
    }
}
