using _123Vendas.Vendas.Application.DTOs;
using _123Vendas.Vendas.Application.Interfaces.Services;
using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Domain.Entities;
using AutoMapper;

namespace _123Vendas.Vendas.Application.Services
{
    public class VendasService : IVendasService
    {
        private readonly IMapper _mapper;
        private readonly IVendasRepository _vendasRepository;

        public VendasService(IMapper mapper, IVendasRepository vendasRepository)
        {
            _mapper = mapper;
            _vendasRepository = vendasRepository;
        }

        public VendaDTO BuscarPorId(Guid id)
        {
            Venda venda = _vendasRepository.BuscarPorIdComProdutos(id);

            if (venda != null)
            {
                return _mapper.Map<VendaDTO>(venda);
            }

            return null;
        }

        public List<VendaDTO> BuscarTodos()
        {
            List<Venda> vendas = _vendasRepository.BuscarTodosComProdutos().ToList();
            return _mapper.Map<List<VendaDTO>>(vendas);
        }

        public VendaDTO Adicionar(VendaDTO vendaDto)
        {
            Venda venda = _mapper.Map<Venda>(vendaDto);
            Venda novaVenda = _vendasRepository.Adicionar(venda);
            return _mapper.Map<VendaDTO>(novaVenda);
        }

        public VendaDTO Atualizar(VendaDTO vendaDto)
        {
            Venda venda = _mapper.Map<Venda>(vendaDto);
            Venda vendaAtualizada = _vendasRepository.Atualizar(venda);
            return _mapper.Map<VendaDTO>(vendaAtualizada);
        }

        public VendaDTO Cancelar(Guid id)
        {
            VendaDTO vendaDto = null;
            Venda venda = _vendasRepository.BuscarPorIdComProdutos(id);

            if (venda != null)
            {
                venda.Cancelado = true;
                venda.Produtos.ForEach(p => p.Cancelado = true);
                _vendasRepository.Atualizar(venda);
                vendaDto = _mapper.Map<VendaDTO>(venda);
            }

            return vendaDto;
        }
    }
}
