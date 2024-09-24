using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Domain.Entities;
using _123Vendas.Vendas.Domain.Repositories;
using _123Vendas.Vendas.Infrastructure.Persistance;

namespace _123Vendas.Vendas.Infrastructure.Repositories
{
    internal class ProdutosRepository : BaseRepository<Produto>, IProdutosRepository
    {
        public ProdutosRepository(VendasDbContext context) : base(context)
        {
        }
    }
}
