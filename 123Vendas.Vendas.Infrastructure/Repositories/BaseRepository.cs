using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _123Vendas.Vendas.Domain.Repositories
{
    internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public readonly VendasDbContext _context;
        public DbSet<TEntity> entity => _context.Set<TEntity>();

        public BaseRepository(VendasDbContext context)
        {
            _context = context;
        }

        public TEntity Adicionar(TEntity entity)
        {
            this.entity.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Atualizar(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public IQueryable<TEntity> BuscarTodos()
        {
            return this.entity.AsNoTracking();
        }

        public TEntity BuscarUm(Expression<Func<TEntity, bool>> predicate)
        {
            return this.entity.AsNoTracking().FirstOrDefault(predicate)!;
        }

        public IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return this.entity.AsNoTracking().Where(predicate);
        }
    }
}