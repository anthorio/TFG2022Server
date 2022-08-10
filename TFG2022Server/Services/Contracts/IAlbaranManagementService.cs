﻿using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IAlbaranManagementService
    {
        Task<List<AlbaranModel>> GetAlbaranes();
        // Task<List<Pedido>> GetPedidos();
    }
}
