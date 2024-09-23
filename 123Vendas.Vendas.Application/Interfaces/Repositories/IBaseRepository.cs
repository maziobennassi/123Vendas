using System.Linq.Expressions;

namespace _123Vendas.Vendas.Application.Services.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        IQueryable<TEntity> BuscarTodos();
        TEntity BuscarUm(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
    }
}
