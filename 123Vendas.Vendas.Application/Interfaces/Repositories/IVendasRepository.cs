﻿using _123Vendas.Vendas.Domain.Entities;

namespace _123Vendas.Vendas.Application.Services.Interfaces
{
    public interface IVendasRepository: IBaseRepository<Venda>
    {
        IQueryable<Venda> BuscarTodosComProdutos();
        Venda BuscarPorIdComProdutos(Guid id);
    }
}