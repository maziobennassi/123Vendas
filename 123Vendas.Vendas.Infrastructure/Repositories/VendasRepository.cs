using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Domain.Entities;
using _123Vendas.Vendas.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Vendas.Domain.Repositories
{
    internal class VendasRepository : BaseRepository<Venda>, IVendasRepository
    {
        public VendasRepository(VendasDbContext context) : base(context)
        {
        }

        public Venda BuscarPorIdComProdutos(Guid id)
        {
            return this.entity.Include(v => v.Produtos).FirstOrDefault(v => v.Id == id);
        }

        public IQueryable<Venda> BuscarTodosComProdutos()
        {
            return this.entity.Include(v => v.Produtos);
        }
    }
}
