using _123Vendas.Vendas.Application.DTOs;
using _123Vendas.Vendas.Application.Interfaces.Services;
using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Domain.Entities;
using AutoMapper;

namespace _123Vendas.Vendas.Application.Services
{
    public class ProdutosService : IProdutosService
    {
        private readonly IMapper _mapper;
        private readonly IProdutosRepository _produtosRepository;

        public ProdutosService(IMapper mapper, IProdutosRepository produtosRepository)
        {
            _mapper = mapper;
            _produtosRepository = produtosRepository;
        }

        public ProdutoDTO BuscarPorId(Guid id)
        {
            Produto produto = _produtosRepository.BuscarUm(p => p.Id == id);

            if (produto != null)
            {
                return _mapper.Map<ProdutoDTO>(produto);
            }

            return null;
        }

        public List<ProdutoDTO> BuscarTodos()
        {
            List<Produto> produtos = _produtosRepository.BuscarTodos().ToList();
            return _mapper.Map<List<ProdutoDTO>>(produtos);
        }

        public ProdutoDTO Cancelar(Guid id)
        {
            ProdutoDTO produtoDto = null;
            Produto produto = _produtosRepository.BuscarUm(p => p.Id == id);

            if (produto != null)
            {
                produto.Cancelado = true;
                _produtosRepository.Atualizar(produto);
                produtoDto = _mapper.Map<ProdutoDTO>(produto);
            }

            return produtoDto;
        }
    }
}
