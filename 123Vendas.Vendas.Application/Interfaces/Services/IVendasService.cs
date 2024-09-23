using _123Vendas.Vendas.Application.DTOs;
using System.Linq.Expressions;

namespace _123Vendas.Vendas.Application.Interfaces.Services
{
    public interface IVendasService
    {
        List<VendaDTO> BuscarTodos();
        VendaDTO BuscarPorId(Guid id);
        void Adicionar(VendaDTO entity);
        void Atualizar(VendaDTO entity);
        VendaDTO Deletar(Guid id);
    }
}