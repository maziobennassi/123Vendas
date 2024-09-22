using _123Vendas.Vendas.Application.DTOs;
using _123Vendas.Vendas.Application.Interfaces.Services;
using _123Vendas.Vendas.Application.Services.Interfaces;
using _123Vendas.Vendas.Domain.Entities;
using AutoMapper;

namespace _123Vendas.Vendas.Application.Services
{
    public class VendasService: IVendasService
    {
        private readonly IMapper _mapper;
        private readonly IVendasRepository _vendasRepository;

        public VendasService(IMapper mapper, IVendasRepository vendasRepository)
        {
            _mapper = mapper;
            _vendasRepository = vendasRepository;
        }

        public void Adicionar(VendaDTO entity)
        {
            Venda model = _mapper.Map<Venda>(entity);
            _vendasRepository.Adicionar(model);
        }
    }
}
