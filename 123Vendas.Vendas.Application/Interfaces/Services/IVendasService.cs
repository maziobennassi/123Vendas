using _123Vendas.Vendas.Application.DTOs;

namespace _123Vendas.Vendas.Application.Interfaces.Services
{
    public interface IVendasService
    {
        List<VendaDTO> BuscarTodos();
        VendaDTO BuscarPorId(Guid id);
        VendaDTO Adicionar(VendaDTO entity);
        VendaDTO Atualizar(VendaDTO entity);
        VendaDTO Cancelar(Guid id);
    }
}