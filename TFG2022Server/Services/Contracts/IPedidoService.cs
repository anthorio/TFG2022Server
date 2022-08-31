﻿using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IPedidoService
    {
        Task<List<PedidoModel>> GetPedidos();
        string[] GetTiposPedido();
        Task CreatePedido(PedidoModel pedidoModel);
    }
}