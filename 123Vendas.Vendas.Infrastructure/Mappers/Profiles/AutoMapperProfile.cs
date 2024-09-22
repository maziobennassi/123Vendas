using _123Vendas.Vendas.Application.DTOs;
using _123Vendas.Vendas.Domain.Entities;
using AutoMapper;

namespace _123Vendas.Vendas.Infrastructure.Mappers.Profiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Venda, VendaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }
}
