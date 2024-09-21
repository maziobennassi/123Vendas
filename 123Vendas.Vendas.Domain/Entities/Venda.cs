﻿namespace _123Vendas.Vendas.Domain.Entities
{
    public class Venda
    {
        public Guid Id { get; set; }
        public string Numero { get; set; } = default!;
        public DateTime Data { get; set; }
        public string Cliente { get; set; } = default!;
        public double ValorTotal { get; set; }
        public string Filial { get; set; } = default!;
        public List<Produto> Produtos { get; set; } = new();
        public bool Cancelado { get; set; }
    }
}
