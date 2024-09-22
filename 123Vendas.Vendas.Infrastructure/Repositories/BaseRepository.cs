using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _123Vendas.Vendas.Domain.Repositories
{
    internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly VendasDbContext _vendasDbContext;

        public BaseRepository(VendasDbContext vendasDbContext)
        {
            _vendasDbContext = vendasDbContext;
        }

        public void Adicionar(TEntity entity)
        {
            _vendasDbContext.Set<TEntity>().Add(entity);
            _vendasDbContext.SaveChanges();
        }

        public void Atualizar(TEntity entity)
        {
            _vendasDbContext.Set<TEntity>().Update(entity);
            _vendasDbContext.SaveChanges();
        }

        public TEntity BuscarUm(Expression<Func<TEntity, bool>> predicate)
        {
            return _vendasDbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate)!;
        }

        public IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return _vendasDbContext.Set<TEntity>().AsNoTracking().Where(predicate);
        }

        public IQueryable<TEntity> BuscarTodos()
        {
            return _vendasDbContext.Set<TEntity>().AsNoTracking();
        }

        public void Deletar(TEntity entity)
        {
            _vendasDbContext.Set<TEntity>().Remove(entity);
            _vendasDbContext.SaveChanges();
        }
    }
}
