namespace _123Vendas.Vendas.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; set; }
        public Guid VendaId { get; set; }
        public string Nome { get; set; } = default!;
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public double Desconto { get; set; }
        public double ValorTotal { get; set; }
        public bool Cancelado { get; set; }
    }
}
