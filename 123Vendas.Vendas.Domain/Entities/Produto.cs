using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123Vendas.Vendas.Domain.Entities
{
    public class Produto
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey("Venda")]
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
        public bool Cancelado { get; set; } = false;
    }
}
