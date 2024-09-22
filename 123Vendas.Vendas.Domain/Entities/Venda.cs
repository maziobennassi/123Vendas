using System.ComponentModel.DataAnnotations;

namespace _123Vendas.Vendas.Domain.Entities
{
    public class Venda
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Numero { get; set; } = default!;

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string Cliente { get; set; } = default!;

        [Required]
        public double ValorTotal { get; set; }

        [Required]
        public string Filial { get; set; } = default!;

        public List<Produto> Produtos { get; set; } = new();

        [Required]
        public bool Cancelado { get; set; } = false;
    }
}
