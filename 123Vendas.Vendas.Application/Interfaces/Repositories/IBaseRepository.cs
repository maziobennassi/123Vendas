using System.Linq.Expressions;

namespace _123Vendas.Vendas.Application.Services.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Adicionar(TEntity entity);
        TEntity Atualizar(TEntity entity);
        IQueryable<TEntity> BuscarTodos();
        TEntity BuscarUm(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
    }
}
