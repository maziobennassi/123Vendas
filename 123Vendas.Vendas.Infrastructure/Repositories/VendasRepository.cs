using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Domain.Entities;
using _123Vendas.Vendas.Infrastructure.Persistance;

namespace _123Vendas.Vendas.Domain.Repositories
{
    internal class VendasRepository : BaseRepository<Venda>, IVendasRepository
    {
        public VendasRepository(VendasDbContext vendasDbContext) : base(vendasDbContext)
        {
        }
    }
}
