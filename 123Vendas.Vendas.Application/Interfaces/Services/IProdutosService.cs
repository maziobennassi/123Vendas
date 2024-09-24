using _123Vendas.Vendas.Application.DTOs;

namespace _123Vendas.Vendas.Application.Interfaces.Services
{
    public interface IProdutosService
    {
        List<ProdutoDTO> BuscarTodos();
        ProdutoDTO BuscarPorId(Guid id);
        ProdutoDTO Cancelar(Guid id);
    }
}
